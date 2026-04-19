using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReagenBack.Core.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("dev-auth")]
public class DevAuthController(IOptions<JwtSettings> jwtOptions) : ControllerBase
{
    private string _key = jwtOptions.Value.Key;

    [HttpPost]
    public IActionResult GenerateToken(string providerUserId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, providerUserId)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }
}