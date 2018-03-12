using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PostalZipService.Services.Identity
{
    public interface IAppUserJwTokenService
    {
       string Create(List<Claim> identityClaims, string jwtKey, string jwtIssuer);
    }
}
