using EWNData.Dto;
using System.Collections.Generic;

namespace EWNData.Repositories.Abstract
{
    public interface IProductRepository
    {
        void Add(IProduct product);
        void AddSpecial(IProduct product);
        IEnumerable<IProduct> GetAll();
        IProduct GetById(int Id);
        string LocalTimeZone { get; set; }
        Product Product { get; set; }
    }
}
