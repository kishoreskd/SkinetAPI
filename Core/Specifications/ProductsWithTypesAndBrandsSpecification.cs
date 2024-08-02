﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(e => e.ProductBrand);
            AddInclude(e => e.ProductType);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id.Equals(id))
        {
            AddInclude(e => e.ProductType);
            AddInclude(e => e.ProductBrand);
        }
    }
}
