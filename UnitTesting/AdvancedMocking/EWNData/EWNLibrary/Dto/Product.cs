namespace EWNData.Dto
{
    public class Product : IProduct
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ReceiptId { get; set; }

        public string ReceiptHeader { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductSpecialStatus SpecialStatus{ get; set; }

        public virtual ICompany Company { get; set; }

        public virtual IProductCategory ProductCategory { get; set; }
       
    }

    public enum ProductSpecialStatus
    {
        Special,
        Default
    }
}
