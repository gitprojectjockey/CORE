using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PostalZipService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var launchHtml = @"Postal Service Zip Web API v.1.0"; 
            return Content(launchHtml);
        }
    }
}