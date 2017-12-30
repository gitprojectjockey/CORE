using EWNData.Dto;
using EWNServices.ServiceModels;
using EWNServices.ProductReceiptServices.Abtract;
using System;

namespace EWNServices.ProductReceiptServices.Concrete
{
    public class ProductReceiptService : IProductReceiptService
    {
        public ProductReceipt CreateReceipt(IProduct product)
        {
            return new ProductReceipt()
            {
                ProductName = product.ProductName,
                ProductCost = product.Price.ToString(),
                PuchaseDate = DateTime.Now
            };
        }

        public ProductReceipt CreateReceipt(IProduct product, out string receiptCode)
        {
            receiptCode = "";
            return new ProductReceipt()
            {
                ProductName = product.ProductName,
                ProductCost = product.Price.ToString(),
                PuchaseDate = DateTime.Now
            };
        }

        public string CreateUniqueReceiptId()
        {
            return "";
        }

        public string CreateReceiptHeader(string productName, string productPrice, string productDesc)
        {
            return $"{productName}::{productPrice}::{productDesc}";
        }
    }
}
