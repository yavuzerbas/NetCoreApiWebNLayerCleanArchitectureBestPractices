using CleanApp.Application;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            return result.Status switch
            {
                System.Net.HttpStatusCode.NoContent => NoContent(),
                System.Net.HttpStatusCode.Created => Created(result.UrlAsCreated, result),
                _ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
            };
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            return result.Status switch
            {
                System.Net.HttpStatusCode.NoContent => NoContent(),
                _ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
            };
        }
    }
}
