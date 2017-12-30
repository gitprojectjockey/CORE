using System;

namespace LMS.WebApi.Exceptions.HandlerHelpers
{
    public class GlobalExceptionFilterResponse
    {
        private readonly ExceptionResult _exceptionResult;
        public GlobalExceptionFilterResponse(Exception ex, string path)
        {
            _exceptionResult = new ExceptionResult(ex.Message, ex.InnerException?.Message, path, ex.Source, ex.StackTrace, ex.HResult);
        }
      
        public ExceptionResult Result => _exceptionResult;

        public class ExceptionResult
        {
            private readonly ExceptionDetails _exDetails;

            public ExceptionResult(string message, string innerMessage, string path, string source,string stackTrace,int hResult)
            {
                _exDetails = new ExceptionDetails()
                {
                    ExHResult = hResult,
                    EndPoint = path,
                    ExMessage = message,
                    ExInnerMessage = innerMessage,
                    ExSource = source,
                    ExStackTrace = stackTrace
                };
            }

            public string Title { get; } = "An Application Error has occured";
            public ExceptionDetails ExDetails { get => _exDetails; }

            public class ExceptionDetails
            {
                public int ExHResult { get; internal set; }
                public string EndPoint { get; internal set; }
                public string ExMessage { get; internal set; }
                public string ExInnerMessage { get; internal set; }
                public string ExSource { get; internal set; }
                public string ExStackTrace { get; internal set; }
                
            }
        }
    }
}
