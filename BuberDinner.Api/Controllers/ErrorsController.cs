using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Api.Controllers;



public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error(){

    Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (statusCode, message) = exception switch
        {

            IServiceException serviceException=> ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred."),
        };


        return Problem(statusCode :statusCode ,title: message);
  }
}
