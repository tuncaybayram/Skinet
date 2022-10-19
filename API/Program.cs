
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddDbContext<StroreContext>(options =>
{
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
var loggerFactory=services.GetRequiredService<ILoggerFactory>();
try
{
    var context =services.GetRequiredService<StroreContext>();
    await context.Database.MigrateAsync();
}
catch(Exception ex)
{
var logger=loggerFactory.CreateLogger<Program>();
logger.LogError(ex,"An error occured during migration");
}
// try
// {
// var context=services.GetRequiredService<StroreContext>();
// await context.Database.MigrateAsync();
// await StroreContextSeed.SeedAASync(context,loggerFactory);
// var userManager=services.GetRequiredService
// }

await app.RunAsync();
