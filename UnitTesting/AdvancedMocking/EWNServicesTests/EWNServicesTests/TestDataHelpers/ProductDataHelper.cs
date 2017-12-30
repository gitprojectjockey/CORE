using EWNData.Dto;
using System.Collections.Generic;

namespace EWNServicesTests.TestDataHelpers
{
    public static class ProductDataHelper
    {
        public static IEnumerable<IProduct> GetProductList()
        {
            List<IProduct> productList = new List<IProduct>()
            {
                new Product()
                {
                    ProductId =1,
                    ProductName ="Sponge Mop",
                    Price = 11.7M,
                    Company = new Company(){CompanyId=21,CompanyName="Spire Cleaning Supplies", State="NY"},
                    ProductCategory = new ProductCategory(){ProductCategoryId =31, CategoryName="Cleaning Supplies" },
                    Description = "Inductrial strength floor mop."
                },
                 new Product()
                {
                    ProductId =2,
                    ProductName ="Wax Stripper",
                    Price = 334.8M,
                    Company = new Company(){CompanyId=22,CompanyName="Puppet Cleaning", State="NV"},
                    ProductCategory = new ProductCategory(){ProductCategoryId =32, CategoryName="Cleaning Supplies" },
                    Description = "Hight grade floor wax."
                },
                  new Product()
                {
                    ProductId =3,
                    ProductName ="Floor Buffer",
                    Price = 367.22M,
                    Company = new Company(){CompanyId=23,CompanyName="USA Cleaning Warehouse", State="TN"},
                    ProductCategory = new ProductCategory(){ProductCategoryId =33, CategoryName="Cleaning Supplies" },
                    Description = "3/4 horse industrial floor buffer and stripper."
                }
                
            };
            return productList;
        }
    }
}
