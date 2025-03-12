using System.Net;
using App.Application;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    protected IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
    {
        return serviceResult.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.Created => Created(serviceResult.UrlAsCreated, serviceResult),
            _ => new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() }
        };
    }

    [NonAction]
    protected IActionResult CreateActionResult(ServiceResult serviceResult)
    {
        return serviceResult.StatusCode switch
        {
            HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() },
            _ => new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() }
        };
    }
}