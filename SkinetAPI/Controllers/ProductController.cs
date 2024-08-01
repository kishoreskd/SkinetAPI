using Core.Entities;
using Core.Specifications;
using Infrastructure.Services.RepositoryService;
using Microsoft.AspNetCore.Mvc;

namespace SkinetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase

    {
        IGenericRepository<Product> _productService;

        public ProductController(IGenericRepository<Product> productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProducts()
        {
            ISpecification<Product> specification = new ProductsWithTypesAndBrandsSpecification();
            return Ok(await _productService.ListAsync(specification));
        }
    }
}
