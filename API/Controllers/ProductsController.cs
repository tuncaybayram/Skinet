using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StroreContext _context;

        public ProductsController(StroreContext context)
        {
            _context=context;
        }
       [HttpGet]
       public async Task <ActionResult<List<Product>>> GetPRoducts()
       {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
       }
        

        [HttpGet("{id}")]
       public async Task<ActionResult<Product>> GetProduct(int id)
         {
             return await _context.Products.FindAsync(id);
         }
        
    }
}