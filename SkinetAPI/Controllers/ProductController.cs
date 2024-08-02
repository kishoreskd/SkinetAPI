using AutoMapper;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Services.RepositoryService;
using Microsoft.AspNetCore.Mvc;
using SkinetAPI.Dto;
using System.Collections.Generic;

namespace SkinetAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        readonly IGenericRepository<Product> _productService;
        readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productService, IMapper mapper)
        {
            this._productService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            Product product = await _productService.GetEntityWithSpec(specification);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProducts()
        {
            ISpecification<Product> specification = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productService.ListAsync(specification);
            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(data);
        }
    }
}
