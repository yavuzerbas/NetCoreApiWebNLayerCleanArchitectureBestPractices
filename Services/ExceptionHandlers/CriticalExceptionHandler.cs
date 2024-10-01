using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Services.ExceptionHandlers
{
    public class CriticalExceptionHandler() : IExceptionHandler
    {
        // if true Here handling the error
        // if false, doing the job then delivering the exception to next handler(GlobalExceptionHandler)
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                Console.WriteLine("An sms related to error sent");
            }
            return ValueTask.FromResult(false);
        }
    }
}
