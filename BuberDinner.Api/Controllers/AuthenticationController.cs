using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

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
        OneOf<AuthenticationResult,DuplicateEmailError> registerResult = _authenticationService.Regiser(request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exist")
        );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.user.Id,
                    authResult.user.FirstName,
                    authResult.user.LastName,
                    authResult.user.Email,
                    authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request){

         var authResult = _authenticationService.Login(
            request.Email,
            request.Password);
        var response = new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token
        );
        return Ok(response);    }
}