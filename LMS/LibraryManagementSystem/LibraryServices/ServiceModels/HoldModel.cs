using System;

namespace LibraryServices.ServiceModels
{
    public class HoldModel
    {
        public int Id { get; set; }
        public virtual LibraryAssetModel LibraryAsset { get; set; }
        public virtual LibraryCardModel LibraryCard { get; set; }
        public DateTime HoldPlaced { get; set; }
    }
}
