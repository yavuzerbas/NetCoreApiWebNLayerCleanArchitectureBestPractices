using CleanApp.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanApp.API.ExceptionHandlers
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
