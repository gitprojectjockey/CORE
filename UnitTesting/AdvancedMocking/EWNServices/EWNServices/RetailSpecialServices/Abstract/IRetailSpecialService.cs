using EWNData.Dto;

namespace EWNServices.RetailSpecialServices.Abstract
{
    public interface IRetailSpecialService
    {
        ProductSpecialStatus GetProductSalesStatus(IProduct product);
        string AdText { get; set; }
    }
}