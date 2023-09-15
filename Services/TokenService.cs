using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAPI.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var claims = new Claim[]
        {
            new Claim("username", user.UserName),
            new Claim("id", user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString(CultureInfo.InvariantCulture))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(30),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token: token);
    }
}