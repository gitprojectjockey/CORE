using LMS.WebApi.Filters.FilterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.WebApi.Filters.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!context.ModelState.IsValid)
            {
                ValidationFilterResultModel validationResult = new ValidationFilterResultModel(context.ModelState);
                var result = new BadRequestObjectResult(validationResult);
                context.Result = result;
            }
        }
    }
}
