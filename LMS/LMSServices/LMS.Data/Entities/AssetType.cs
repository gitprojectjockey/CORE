using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    public class AssetType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
