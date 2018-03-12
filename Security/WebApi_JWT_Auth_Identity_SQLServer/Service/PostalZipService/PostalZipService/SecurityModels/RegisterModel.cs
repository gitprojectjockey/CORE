using System.ComponentModel.DataAnnotations;

namespace PostalZipService.SecurityModels
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string SalesRegion { get; set; }
    }
}
