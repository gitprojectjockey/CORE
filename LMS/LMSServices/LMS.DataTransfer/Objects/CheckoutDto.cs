using System;
using System.ComponentModel;

namespace LMS.DataTransfer.Objects
{
    [Identity("CheckoutDto")]
    public class CheckoutDto
    {
        public int Id { get; set; }
        public LibraryAssetDto LibraryAsset { get; set; }
        public LibraryCardDto LibraryCard { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}
