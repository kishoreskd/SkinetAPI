using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(e => e.ProductBrand);
            AddInclude(e => e.ProductType);
            AddOrderBy(e => e.Name);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id.Equals(id))
        {
            AddInclude(e => e.ProductType);
            AddInclude(e => e.ProductBrand);
        }
    }
}
