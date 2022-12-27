using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Presistence;
using BuberDinner.Domain.Entities;
using OneOf;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user){
            throw new Exception("this user is not registered");
        }
        if(user.Password != password){
            throw new Exception("user or password is wrong");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public OneOf<AuthenticationResult,DuplicateEmailError> Regiser(string firstName, string lastName, string email, string password)
    {
        // check if user already exist
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            return new DuplicateEmailError();
        }
        //create user with unique id 
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);
        //generate the token 

        var token = _jwtTokenGenerator.GenerateToken(user);

        
        return new AuthenticationResult(user, token);
    }
}
