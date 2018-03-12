using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}