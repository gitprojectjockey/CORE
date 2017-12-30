using System;
using System.Collections.Generic;

namespace LibraryServices.ServiceModels
{
    public class LibraryBranchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public virtual IEnumerable<PatronModel> Patrons { get; set; }
        public virtual IEnumerable<LibraryAssetModel> LibraryAssets { get; set; }
        public string ImageUrl { get; set; }
    }
}
