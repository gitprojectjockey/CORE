using LibraryServices.ServiceModels;
using System.Collections.Generic;

namespace LMS.ViewModels.Catalog
{
    public class AssetDetailViewModel
    {
        public int AssetId { get; set; }
        public string Title { get; set; }
        public string AuthorOrDirector{ get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string DeweyCallNumber{ get; set; }
        public string Status { get; set; }
        public decimal Cost{ get; set; }
        public string CurrentLocation { get; set; }
        public string ImageUrl { get; set; }
        public string PatronName { get; set; }
        public CheckoutViewModel LatestCheckout { get; set; }
        public LibraryCardModel CurrentAssociatedLibraryCard { get; set; }
        public IEnumerable<CheckoutHistoryModel> CheckoutHistory{ get; set; }
        public IEnumerable<AssetHoldViewModel> CurrentHolds { get; set; }
    }
}
