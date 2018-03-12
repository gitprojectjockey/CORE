using System;

namespace JsonWebToken_WebApi.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
