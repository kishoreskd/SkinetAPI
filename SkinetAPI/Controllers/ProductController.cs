using AutoMapper;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Services.RepositoryService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkinetAPI.Dto;
using SkinetAPI.Errors;
using System.Collections.Generic;

namespace SkinetAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        readonly IGenericRepository<ProductBrand> _productBrandRepo;
        readonly IGenericRepository<ProductType> _productTypeRepo;
        readonly IGenericRepository<Product> _productService;
        readonly IMapper _mapper;

        public ProductController(
            IGenericRepository<Product> productService,
            IGenericRepository<ProductType> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IMapper mapper)
        {
            this._productService = productService;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        //this below attributes helps to swagger for documents whether what are the response can get from the action
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            Product product = await _productService.GetEntityWithSpec(specification);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            ISpecification<Product> specification = new ProductsWithTypesAndBrandsSpecification(productParams);
            var products = await _productService.ListAsync(specification);
            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(data);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}
