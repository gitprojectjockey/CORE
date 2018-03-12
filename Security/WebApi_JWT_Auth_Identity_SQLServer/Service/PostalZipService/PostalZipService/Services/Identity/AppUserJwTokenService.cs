using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostalZipService.Services.Identity
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
    public class AppUserJwTokenService : IAppUserJwTokenService
    {
        public string Create(List<Claim> identityClaims, string jwtKey, string jwtIssuer)
        {
            List<Claim> jwtClaims = new List<Claim>();

            foreach (var c in identityClaims)
            {
                switch (c.Type)
                {
                    case "user.email":
                        jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Email, c.Value));
                        break;
                    case "user.name":
                        jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, c.Value));
                        break;
                    case "user.sales.region":
                        jwtClaims.Add(new Claim("user.sales.region", c.Value));
                        break;
                    default:
                        break;
                }
            }
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwtIssuer, jwtIssuer, jwtClaims, expires: DateTime.Now.AddMinutes(300), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
