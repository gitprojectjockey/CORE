namespace LibraryServices.ServiceModels
{
    public class BookModel : LibraryAssetModel
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string DeweyIndex { get; set; }
    }
}
