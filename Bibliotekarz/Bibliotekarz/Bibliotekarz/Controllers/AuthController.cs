using Bibliotekarz.Shared.DTOs;
using Bibliotekarz.Shared.ResultWrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bibliotekarz.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        IdentityUser user = new()
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(Result.Fail(result.Errors.Select(e => e.Description).ToList()));
        }

        await userManager.AddToRoleAsync(user, "BookViewer");
        return Ok();
    }

    public async Task<IActionResult> Login(LoginDto request)
    {
        //Uwierzytelnianie użytkownika
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return Unauthorized();

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized();

        // Tworzenie tokena JWT
        var userRoles = await userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

        var jwtSection = configuration.GetSection("Jwt");
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            expires: DateTime.UtcNow.AddMinutes(60),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            roles = userRoles
        });
    }
}
