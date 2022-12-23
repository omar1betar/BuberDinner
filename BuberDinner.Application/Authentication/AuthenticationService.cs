namespace BuberDinner.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid() ,"firstName", "lastName", email, "token");
    }

    public AuthenticationResult Regiser(string firstName, string lastName, string email, string password)
    {
        return new AuthenticationResult( Guid.NewGuid() ,firstName, lastName, email, "token");
    }
}
