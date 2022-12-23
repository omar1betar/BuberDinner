namespace BuberDinner.Application.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Regiser(string firstName,string lastName,string email,string password);
    AuthenticationResult Login(string email,string password);

}
