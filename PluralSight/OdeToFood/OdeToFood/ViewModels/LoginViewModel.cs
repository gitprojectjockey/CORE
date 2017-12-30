using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OdeToFood.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("User Name")]
        [Required, MaxLength(256)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password), MaxLength(20)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
