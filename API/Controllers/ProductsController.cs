using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
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
       public async Task <ActionResult<List<ProductToReturnDto>>> GetPRoducts()
       {
        var spec =new ProductsWithTypesAndBrandsSpecification();

            var products=await _producsRepo.ListAsync(spec);
            return products.Select(product=>new ProductToReturnDto
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                PictureUrl=product.PictureUrl,
                Price=product.Price,
                ProductBrand=product.ProductBrand.Name,
                ProductType=product.ProductType.Name
            
            }).ToList();
       }
        

        [HttpGet("{id}")]
       public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
         {
            var spec =new ProductsWithTypesAndBrandsSpecification(id);
            var product= await _producsRepo.GetEntityWithSpec(spec);
            return new ProductToReturnDto
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                PictureUrl=product.PictureUrl,
                Price=product.Price,
                ProductBrand=product.ProductBrand.Name,
                ProductType=product.ProductType.Name
            
            };
         }

         [HttpGet("brands")]
         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
         {
             return Ok(await _producBrandsRepo.ListAllAsync());
         }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
         {
             return Ok(await _producTypesRepo.ListAllAsync());
         }
    }
}