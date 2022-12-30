using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Api.Controllers;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Application.Services.Authentication.Common;

namespace BuberDinner.Api.Controllers;

//[ApiController]
[Route("auth")]
public class AuthenticationController :ApiController
{
private readonly IAuthenticationQueryService _authenticationQueryService;
private readonly IAuthenticationCommandService _authenticationCommandService;

    public AuthenticationController(IAuthenticationQueryService authenticationQueryService,
     IAuthenticationCommandService authenticationCommandService = null!)
    {
        _authenticationQueryService = authenticationQueryService;
        _authenticationCommandService = authenticationCommandService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request){
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Regiser(
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

        var authResult = _authenticationQueryService.Login(
           request.Email,
           request.Password);
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }

}