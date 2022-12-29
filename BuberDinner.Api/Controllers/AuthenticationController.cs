using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Api.Controllers;

namespace BuberDinner.Api.Controllers;

//[ApiController]
[Route("auth")]
public class AuthenticationController :ApiController
{
private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request){
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Regiser(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
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
    public IActionResult Login(LoginRequest request)
    {

        var authResult = _authenticationService.Login(
           request.Email,
           request.Password);
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }

}