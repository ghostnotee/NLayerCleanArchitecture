using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace App.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
    {
        return serviceResult.StatusCode == HttpStatusCode.NoContent
            ? new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() }
            : new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() };
    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult serviceResult)
    {
        return serviceResult.StatusCode == HttpStatusCode.NoContent
            ? new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() }
            : new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() };
    }
}