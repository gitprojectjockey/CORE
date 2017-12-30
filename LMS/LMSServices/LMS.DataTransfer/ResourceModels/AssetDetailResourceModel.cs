using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.ResourceModels
{
    public class AssetDetailResourceModel
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
        public AssetCheckoutResourceModel LatestCheckout { get; set; }
        public LibraryCardDto CurrentAssociatedLibraryCard { get; set; }
        public IEnumerable<CheckoutHistoryDto> CheckoutHistory { get; set; }
        public IEnumerable<AssetHoldResourceModel> CurrentHolds { get; set; }
    }
}
