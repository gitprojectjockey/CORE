using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    public class AboutController : Controller
    {
        public ContentResult Phone()
        {
            return Content("555-555-5555");
        }

        public ContentResult Country()
        {
            return Content("USA");
        }
    }
}
