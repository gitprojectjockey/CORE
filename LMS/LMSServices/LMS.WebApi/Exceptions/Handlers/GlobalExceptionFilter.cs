using LMS.WebApi.Exceptions.HandlerHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace LMS.WebApi.Exceptions.Handlers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GlobalExceptionFilter>(); ;
        }
        public void OnException(ExceptionContext context)
        {
            var logMessage = $"Url:{context.HttpContext.Request.Path}\r\n\r\nExceptionMessage:: {context.Exception.Message}\r\n\r\nStackTrace:: {context.Exception.StackTrace}\r\n\r\n";
            _logger.LogCritical(logMessage);
            var response = new GlobalExceptionFilterResponse(context.Exception, context.HttpContext.Request.Path);

            context.Result = new JsonResult(response.Result) {StatusCode = 500};
        }
    }
}

