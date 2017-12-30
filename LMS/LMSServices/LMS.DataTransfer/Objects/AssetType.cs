namespace LMS.DataTransfer.Objects
{
    [Identity("AssetTypeDto")]
    public class AssetType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
