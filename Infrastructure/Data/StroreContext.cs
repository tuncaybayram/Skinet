using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StroreContext : DbContext
    {
        public StroreContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}