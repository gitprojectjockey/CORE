using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    public class Greeting : ViewComponent
    {
        private IGreeterService _greeter;
        public Greeting(IGreeterService greeter)
        {
            _greeter = greeter;
        }

#pragma warning disable 1998
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _greeter.GetGreeting();
            return View("Default", model);
        }
    }
}
