using EWNData.Dto;
using EWNServices.RetailSpecialServices.Abstract;
using EWNServices.UserDefinedExceptions;

namespace EWNServices.RetailSpecialServices.Concrete
{
    public class RetailSpecialService : IRetailSpecialService
    {
        public string TimezoneDisplay { get; set; }

        public string AdText { get; set; }

        public ProductSpecialStatus GetProductSalesStatus(IProduct product)
        {
            if (product.Price <= 0)
            {
                throw new ProductPriceException("Product price cannot be less than or equal to zero");
            }
            if (product.Price < 30.22m)
            {
                return ProductSpecialStatus.Special;
            }
            return ProductSpecialStatus.Default;
        }

        
    }
}
