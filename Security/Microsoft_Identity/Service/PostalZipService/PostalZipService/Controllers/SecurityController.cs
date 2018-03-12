using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostalZipService.SecurityModels;
using PostalZipService.Services.Email;
using PostalZipService.Services.Identity;
using System.Net;
using System.Threading.Tasks;

namespace PostalZipService.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IEMailSender _emailSender;
        public SecurityController(UserManager<AppIdentityUser> userManager,
                                  SignInManager<AppIdentityUser> signInManager,IEMailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Age = model.Age
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);

            if (!identityResult.Succeeded)
            {
                // You need to handle error well and understand it
                // Need to GET identity Errors and pass back correctly
                return BadRequest(ModelState);
            }

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                //if (!await _userManager.IsEmailConfirmedAsync(user))
                //{
                //    ModelState.AddModelError(string.Empty,
                //              "Confirm your email please");
                //    return View(model);
                //}
            }

            var signInResult = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                // You need to handle error well and understand it
                // Need to GET signInResult Errors and pass back correctly
                return BadRequest(ModelState);
            }

            ModelState.AddModelError(string.Empty, "Login Failed");

            var resultSuccess = new JsonResult(signInResult)
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
