using System;
using System.Collections.Generic;

namespace LMS.DataTransfer.Objects
{
    [Identity("LibraryBranchDto")]
    public class LibraryBranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public virtual IEnumerable<PatronDto> Patrons { get; set; }
        public virtual IEnumerable<LibraryAssetDto> LibraryAssets { get; set; }
        public string ImageUrl { get; set; }
    }
}
