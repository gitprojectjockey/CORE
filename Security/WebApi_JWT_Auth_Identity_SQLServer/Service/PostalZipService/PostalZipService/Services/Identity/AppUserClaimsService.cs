using Microsoft.AspNetCore.Identity;
using PostalZipService.SecurityModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostalZipService.Services.Identity
{
    public class AppUserClaimsService : IAppUserClaimsService
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        public AppUserClaimsService(UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserClaims(AppIdentityUser currentUser, RegisterModel registerModel)
        {
            var identityUser = await _userManager.FindByEmailAsync(currentUser.Email);
            var passwordOk = await _userManager.CheckPasswordAsync(identityUser, registerModel.Password);

            if (passwordOk)
            {
                var claims = await _userManager.GetClaimsAsync(identityUser);
                if (claims.Count == 0)
                {
                    var userEmailClaimResult = await _userManager.AddClaimAsync(identityUser, new Claim("user.email", currentUser.Email));
                    var userNameClaimResult = await _userManager.AddClaimAsync(identityUser, new Claim("user.name", currentUser.UserName));
                    var userDistrictClaimResult = await _userManager.AddClaimAsync(identityUser, new Claim("user.sales.region", registerModel.SalesRegion));
                }
            }
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(identityUser);
            return claims.ToList();
        }
    }
}
