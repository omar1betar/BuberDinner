using FluentResults;

namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    Result<AuthenticationResult> Regiser(string firstName,string lastName,string email,string password);
    AuthenticationResult Login(string email,string password);

}
