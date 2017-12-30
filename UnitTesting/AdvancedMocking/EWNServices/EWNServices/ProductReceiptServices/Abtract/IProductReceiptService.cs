using EWNData.Dto;
using EWNServices.ServiceModels;

namespace EWNServices.ProductReceiptServices.Abtract
{
    public interface IProductReceiptService
    {
        ProductReceipt CreateReceipt(IProduct product);
        ProductReceipt CreateReceipt(IProduct product, out string receiptCode);
        string CreateUniqueReceiptId();
        string CreateReceiptHeader(string productName,string productPrice, string productDesc);
    }
}
