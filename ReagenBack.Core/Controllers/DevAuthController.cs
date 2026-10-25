using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReagenBack.Core.Configurations;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("dev-auth")]
// Gera token sem senha, apenas para desenvolvimento
public class DevAuthController(
    IOptions<JwtSettings> jwtOptions,
    IWebHostEnvironment environment,
    UserService userService
) : ControllerBase
{
    private string _key = jwtOptions.Value.Key;

    [HttpPost]
    public async Task<IActionResult> GenerateToken(string providerUserId)
    {
        if (!environment.IsDevelopment())
            return NotFound();

        // Cria o usuário na primeira vez, para o token já ser utilizável
        if (await userService.GetUserAsync(providerUserId) == null)
            await userService.AddUserAsync(providerUserId, new UserDto { Name = providerUserId });

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