namespace LMS.DataTransfer.Objects
{
    [Identity("VideoDto")]
    public class VideoDto : LibraryAssetDto
    {
        public string Director { get; set; }
    }
}
