using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PostalZipService.SecurityModels;
using PostalZipService.Services.Identity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PostalZipService.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
       
        private readonly IAppUserClaimsService _userClaimsService;
        private readonly IAppUserJwTokenService _userJwToken;
        private readonly IConfiguration _config;
        public SecurityController(UserManager<AppIdentityUser> userManager,
                                  SignInManager<AppIdentityUser> signInManager,
                                  IAppUserClaimsService userClaimsService,
                                  IAppUserJwTokenService userJwToken,
                                  IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsService = userClaimsService;
            _userJwToken = userJwToken;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            //This is taken care of by a ActionFilter
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var user = new AppIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);
            
            if (!identityResult.Succeeded)
            {
                var resultRegisterFailed = new JsonResult(identityResult.Errors)
                {
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return resultRegisterFailed;
            }

            await _userClaimsService.AddUserClaims(user, model);

            var resultSuccess = new JsonResult(identityResult)
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };

            return resultSuccess;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //This is taken care of by a ActionFilter
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var signInResult = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                var resultLoginFailed = new JsonResult(signInResult.IsNotAllowed)
                {
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.Forbidden
                };
                return resultLoginFailed;
            }
           
            var userClaims = await _userClaimsService.GetUserClaims(model.Email);
            var tokenString = _userJwToken.Create(userClaims.ToList(),_config["jwt:Key"],_config["jwt:Issuer"]);

            var resultSuccess = new JsonResult(new { token = tokenString })
            {
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };

            // In this authentication scenario we will logout Immediately because the client user will be authenticating via a JWToken for the rest 
            // of the requests.
            Logout();

            return resultSuccess;
        }

        // In this authentication scenario we will logout Immediately because the client user will be authenticating via a JWToken for the rest 
        // of the requests.
        private async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
