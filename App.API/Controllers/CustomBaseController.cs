using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
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
                System.Net.HttpStatusCode.Created => Created(result.UrlAsCreated, result.Data),
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
