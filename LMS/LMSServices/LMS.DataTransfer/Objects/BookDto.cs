namespace LMS.DataTransfer.Objects
{
    [Identity("BookDto")]
    public class BookDto : LibraryAssetDto
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string DeweyIndex { get; set; }
    }
}
