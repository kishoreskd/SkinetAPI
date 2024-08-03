using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) :
            base(x =>
            !productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId &&
            !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        {
            AddInclude(e => e.ProductBrand);
            AddInclude(e => e.ProductType);
            AddOrderBy(e => e.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc": AddOrderBy(e => e.Price); break;
                    case "priceDesc": AddOrderByDecending(e => e.Price); break;
                    default: AddOrderBy(e => e.Name); break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id.Equals(id))
        {
            AddInclude(e => e.ProductType);
            AddInclude(e => e.ProductBrand);
        }
    }
}
