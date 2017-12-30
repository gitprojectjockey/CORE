using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
