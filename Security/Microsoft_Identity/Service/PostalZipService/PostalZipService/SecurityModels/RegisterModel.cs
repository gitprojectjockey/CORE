using System;
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

        [Required]
        public string Email { get; set; }

        public int Age { get; set; }
    }
}
