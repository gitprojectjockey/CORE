using System.ComponentModel.DataAnnotations;

namespace PostalZipService.SecurityModels
{
    public class ResetPasswordModel
    {
        public string Code { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
