using System;

namespace LMS.DataTransfer.Objects
{
    [Identity("HoldDto")]
    public class HoldDto
    {
        public int Id { get; set; }
        public virtual LibraryAssetDto LibraryAsset { get; set; }
        public virtual LibraryCardDto LibraryCard { get; set; }
        public DateTime HoldPlaced { get; set; }
    }
}
