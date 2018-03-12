using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace PostalZipService.ActionFilters
{

    // ASP.NET MVC will automatically validate our model for us, and will return a 400 Bad Request response for anything that’s invalid.
    // Furthermore, we can define what “invalid” means with simple attributes on our data models/DTOs.
    public class ValidateModelActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestObjectResult(
                    context.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage));
            }
            base.OnActionExecuting(context);
        }
    }
}
