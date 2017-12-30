using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    public class Video : LibraryAsset
    {
        [Required]
        public string Director { get; set; }
    }
}
