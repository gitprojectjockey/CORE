using System.Collections.Generic;

namespace LibraryServices.ServiceModels
{
    public class LibraryAssetDetailModel
    {
        public int AssetId { get; set; }
        public string Title { get; set; }
        public string AuthorOrDirector { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string Dewey { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public string CurrentLocation { get; set; }
        public string ImageUrl { get; set; }
        public string PatronName { get; set; }
        public LibraryAssetCheckoutModel LatestCheckout { get; set; }
        public LibraryCardModel CurrentAssociatedLibraryCard { get; set; }
        public IEnumerable<CheckoutHistoryModel> CheckoutHistory { get; set; }
        public IEnumerable<LibraryAssetHoldModel> CurrentHolds { get; set; }
    }
}
