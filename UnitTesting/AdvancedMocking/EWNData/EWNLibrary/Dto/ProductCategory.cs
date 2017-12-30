using System;
using System.Collections.Generic;

namespace EWNData.Dto
{
    public class ProductCategory : IProductCategory
    {
        public int ProductCategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
