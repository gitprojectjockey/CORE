using System.ComponentModel.DataAnnotations;

namespace PostalZipService.SecurityModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
