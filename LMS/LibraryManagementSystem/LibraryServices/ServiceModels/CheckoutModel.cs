using System;

namespace LibraryServices.ServiceModels
{
    public class CheckoutModel
    {
        public int Id { get; set; }
        public LibraryAssetModel LibraryAsset { get; set; }
        public LibraryCardModel LibraryCard { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}
