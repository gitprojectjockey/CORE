using System.Collections.Generic;
using EWNData.Dto;
using EWNData.Repositories.Abstract;

namespace EWNData.Repositories.Concrete
{
    public class ProductRespository : IProductRepository
    {
        public string LocalTimeZone
        {
            get { return System.TimeZoneInfo.Local.DisplayName;  }
            set { LocalTimeZone = value; }
        }

        public Product Product { get; set; }

        public void Add(IProduct product){}

        public void AddSpecial(IProduct product)
        {
            // This will save a product that is on special
        }

        public IEnumerable<IProduct> GetAll()
        {
            return new List<Product>();
        }

        public IProduct GetById(int Id)
        {
            return new Product() { };
        }
    }
}
