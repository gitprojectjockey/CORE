using EWNData.Dto;
using System.Collections.Generic;

namespace EWNServices.ProductServices.Abstract
{
    public interface IProductService
    {
        void Save(IProduct product);
        void SaveRange(IEnumerable<IProduct> products);
        void SaveWithReceipt(IProduct product);
        void SaveWithReceipt(IProduct product, out string receiptCode);
        void SaveWithReceipt(List<IProduct> product);
        void SaveWithReceiptHeader(IProduct product);
        void SaveWithProductSpecialStatus(IProduct product);
        void SaveWithAdText(IProduct product);
    }
}