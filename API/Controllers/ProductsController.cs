using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        IGenericRepository<Product> _producsRepo;
        IGenericRepository<ProductType> _producTypesRepo;
        IGenericRepository<ProductBrand> _producBrandsRepo;

        public ProductsController(IGenericRepository<ProductBrand> producBrandsRepo, IGenericRepository<ProductType> producTypesRepo, IGenericRepository<Product> producsRepo)
        {
            _producBrandsRepo = producBrandsRepo;
            _producTypesRepo = producTypesRepo;
            _producsRepo = producsRepo;
        }

        [HttpGet]
       public async Task <ActionResult<List<Product>>> GetPRoducts()
       {
            var products=await _producsRepo.ListAllAsync();
            return Ok(products);
       }
        

        [HttpGet("{id}")]
       public async Task<ActionResult<Product>> GetProduct(int id)
         {
            return await _producsRepo.GetByIdAsync(id);
         }

         [HttpGet("brands")]
         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
         {
             return Ok(await _repo.GetProductBrandsAsync());
         }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
         {
             return Ok(await _repo.GetProductTypesAsync());
         }
    }
}