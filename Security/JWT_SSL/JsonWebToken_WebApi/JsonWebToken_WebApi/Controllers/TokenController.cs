using JsonWebToken_WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



// The first thing to notice is the presence of AllowAnonymous attribute. 
// This is very important, since this must be a public API, that is an API that anyone can access to get a new token after providing his credentials.
// The API responds to an HTTP POST request and expects an object containing username and password(a LoginModel object).

// The Authenticate method verifies that the provided username and password are the expected ones and returns a User object representing the user.
// Of course, this is a trivial implementation of the authentication process. A production-ready implementation should be more accurate as we know.

// If the Authentication method returns a user, that is the provided credentials are valid, the API generates a new token via the BuildToken method.
// And this is the most interesting part: here we create a JSON Web Token by using the JwtSecurityToken class. We pass a few parameters to the class constructor,
// such as the issuer, the audience(in our case both are the same), the expiration date and time and the signature.
// Finally, the BuildToken method returns the token as a string, by converting it through the WriteToken method of the JwtSecurityTokenHandler class.

namespace JsonWebToken_WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        
        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]Login login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(User user)
        {
            // A token may contain some data called claims. These are usually information about the user 
            // that can be useful when authorizing the access to a resource. 
            // Claims could be, for example, user's e-mail, gender, role, city, or any other information useful to 
            // discriminate users while accessing to resources. 
            // We can add claims in a JWT so that they will be available while checking authorization to access a resource. 

            // We should include in the JWT returned after the authentication, information about the user's age. 

            // The claims variable is an array of Claims instances, each created from a key and a value. 
            // The keys are values of a structure (JwtRegisteredClaimNames) that provides names for public standardized claims. 
            // We created claims for the user's name, email, birthday and for a unique identifier associated to the JWT.
            // This claims array is then passed to the JwtSecurityToken constructor so that it will be included in the JWT sent to the client.

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(Login login)
        {
            // In Production we would get this from a user store.

            User user = null;

            if (login.Username == "enordin" && login.Password == "Wr400fg!")
            {
                user = new User { Name = "Eric Nordin", Email = "ewn@comcast.com",Birthdate = new DateTime(1962, 5, 13) };
            }

            if (login.Username == "valerie" && login.Password == "Wr400fg!")
            {
                user = new User { Name = "Valerie Nordin", Email = "val.comcast.com", Birthdate = new DateTime(2010, 5, 13) };
            }

            return user;
        }
    }
}