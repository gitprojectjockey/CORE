namespace LibraryServices.ServiceModels
{
    public class LibraryAssetModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; } // Just store as an int for BC
        public StatusModel Status { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        public virtual LibraryBranchModel Location { get; set; }
    }
}
