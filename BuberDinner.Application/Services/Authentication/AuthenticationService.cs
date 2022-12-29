using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Presistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using FluentResults;

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

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user){
            return Errors.Authentication.InvalidCredentials;
        }
        if(user.Password != password){
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Regiser(string firstName, string lastName, string email, string password)
    {
        // check if user already exist
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
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
