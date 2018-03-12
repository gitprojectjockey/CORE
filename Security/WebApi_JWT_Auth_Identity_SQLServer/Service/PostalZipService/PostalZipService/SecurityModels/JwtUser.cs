using System;

namespace PostalZipService.SecurityModels
{
    public class JwtUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
