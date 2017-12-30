using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace LMS.WebApi.Filters.FilterHelpers
{
    public class ValidationFilterResultModel
    {
        public string Message { get; }
        public List<ValidationError> Errors { get; }

        public ValidationFilterResultModel(ModelStateDictionary modelState)
        {
            Message = "Model Validation Failed";
            Errors= modelState.Keys
                   .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                   .ToList();
        }
    }
}
