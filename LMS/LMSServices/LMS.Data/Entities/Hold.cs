using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    public class Hold
    {
        [Key]
        public int Id { get; set; }

        public virtual LibraryAsset LibraryAsset { get; set; }

        public virtual LibraryCard LibraryCard { get; set; }

        public DateTime HoldPlaced { get; set; }
    }
}
