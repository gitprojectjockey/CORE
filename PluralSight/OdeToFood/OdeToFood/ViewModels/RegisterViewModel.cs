using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("User Name")]
        [Required, MaxLength(256)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Compare(nameof(Password)), DataType(DataType.Password), MaxLength(20)]
        public string ConfirmPassword { get; set; }
    }
}
