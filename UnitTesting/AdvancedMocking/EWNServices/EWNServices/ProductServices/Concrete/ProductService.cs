using EWNData.Dto;
using EWNData.Repositories.Abstract;
using EWNServices.ProductReceiptServices.Abtract;
using EWNServices.ProductServices.Abstract;
using EWNServices.RetailSpecialServices.Abstract;
using EWNServices.ServiceModels;
using EWNServices.UserDefinedExceptions;
using System;
using System.Collections.Generic;

namespace EWNServices.ProductService.Concrete
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IProductReceiptService _productReceiptService;
        IRetailSpecialService _retailSpecialService;
        public ProductService(IProductRepository productRepository,
                              IProductReceiptService productReceiptService,
                              IRetailSpecialService retailSpecialService)
        {
            _productRepository = productRepository;
            _productReceiptService = productReceiptService;
            _retailSpecialService = retailSpecialService;
        }

        public void Save(IProduct product)
        {
            _productRepository.LocalTimeZone = TimeZoneInfo.Local.DisplayName;
            _productRepository.Add(product);
        }

        public void SaveRange(IEnumerable<IProduct> products)
        {
            foreach (var product in products)
            {
                _productRepository.Add(product);
            }
            var ltz = _productRepository.LocalTimeZone;
            var companyName = _productRepository.Product?.Company.CompanyName;
        }

        public void SaveWithProductSpecialStatus(IProduct product)
        {
            var status = _retailSpecialService.GetProductSalesStatus(product);

            if (status == ProductSpecialStatus.Special)
            {
                product.SpecialStatus = ProductSpecialStatus.Special;
                _productRepository.AddSpecial(product);
            }
            else
            {
                product.SpecialStatus = ProductSpecialStatus.Default;
                _productRepository.Add(product);
            }
        }

        public void SaveWithReceipt(IProduct product)
        {
            try
            {
                ProductReceipt receipt = _productReceiptService.CreateReceipt(product);

                if (receipt == null)
                {
                    throw new InvalidProductReceiptException("Create Receipt Method Returned Null");
                }

                _productRepository.Add(product);
            }
            catch (ProductPriceException)
            {
                throw new SaveWithReceiptException("Failed to Save with Reciept");
            }
            catch (InvalidProductReceiptException)
            {
                throw new SaveWithReceiptException("Failed to Save with Reciept");
            }
            catch (Exception)
            {
                throw new SaveWithReceiptException("Failed to Save with Reciept");
            }
        }

        public void SaveWithReceipt(IProduct product, out string receiptCode)
        {
            receiptCode = "";
            ProductReceipt receipt = _productReceiptService.CreateReceipt(product, out receiptCode);

            if (receipt != null)
            {
                _productRepository.Add(product);
            }
        }

        public void SaveWithReceipt(List<IProduct> products)
        {
            foreach (var product in products)
            {
                product.ReceiptId = _productReceiptService.CreateUniqueReceiptId().ToString();
                _productRepository.Add(product);
            }
        }

        public void SaveWithReceiptHeader(IProduct product)
        {
            string receiptHeader = _productReceiptService.CreateReceiptHeader(
                product.ProductName,
                product.Price.ToString(),
                product.Description);

            product.ReceiptHeader = receiptHeader;
            _productRepository.Add(product);
        }

        public void SaveWithAdText(IProduct product)
        {
            string addText = _retailSpecialService.AdText;

            if (string.IsNullOrEmpty(addText))
            {
                throw new SaveWithAdTextException($"{nameof(SaveWithAdText)} failed because add text value is null");
            }
           
            _productRepository.Add(product);
        }
    }
}
