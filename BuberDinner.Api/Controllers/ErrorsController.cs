using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Api.Controllers;



public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error(){

   

        return Problem();
  }
}
