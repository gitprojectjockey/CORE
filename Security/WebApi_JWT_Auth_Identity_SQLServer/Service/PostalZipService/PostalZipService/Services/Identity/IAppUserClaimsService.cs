using PostalZipService.SecurityModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostalZipService.Services.Identity
{
    public interface IAppUserClaimsService
    {
        Task AddUserClaims(AppIdentityUser currentUser, RegisterModel registerModel);
        Task<IEnumerable<Claim>> GetUserClaims(string email);
    }
}
