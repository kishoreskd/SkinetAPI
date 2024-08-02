using Core.Entities;
using Core.Specifications;
using Infrastructure.Services.RepositoryService;
using Microsoft.AspNetCore.Mvc;

namespace SkinetAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        IGenericRepository<Product> _productService;

        public ProductController(IGenericRepository<Product> productService)
        {
            this._productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            return Ok(await _productService.GetEntityWithSpec(specification));
        }

        [HttpGet("products")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProducts()
        {
            ISpecification<Product> specification = new ProductsWithTypesAndBrandsSpecification();
            return Ok(await _productService.ListAsync(specification));
        }
    }
}
