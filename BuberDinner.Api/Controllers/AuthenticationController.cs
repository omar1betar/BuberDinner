using BuberDinner.Application.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController :ControllerBase
{
private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request){
        var authResult = _authenticationService.Regiser(request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        return Ok(request);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request){
        return Ok(request);
    }
}