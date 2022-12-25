using System.Text;
using System.Security.Claims;
using BuberDinner.Application.Common.Interface.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using BuberDinner.Application.Common.Interface.Services;
using Microsoft.Extensions.Options;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;



    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider,IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var singingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
            

        
        var claims = new[]
        {
            new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim (JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var SecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience :_jwtSettings.Audience,
            expires: _dateTimeProvider.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: singingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(SecurityToken);
    }
}
