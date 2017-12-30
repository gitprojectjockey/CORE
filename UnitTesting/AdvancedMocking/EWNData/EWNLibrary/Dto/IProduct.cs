namespace EWNData.Dto
{
    public interface IProduct
    {
        ICompany Company { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        IProductCategory ProductCategory { get; set; }
        int ProductId { get; set; }
        string ProductName { get; set; }
        string ReceiptHeader { get; set; }
        string ReceiptId { get; set; }
        ProductSpecialStatus SpecialStatus { get; set; }
    }
}