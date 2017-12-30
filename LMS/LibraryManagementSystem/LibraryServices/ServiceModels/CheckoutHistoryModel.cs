using System;

namespace LibraryServices.ServiceModels
{
    public class CheckoutHistoryModel
    {
        public int Id { get; set; }
        public LibraryAssetModel LibraryAsset { get; set; }
        public LibraryCardModel LibraryCard { get; set; }
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
    }
}
