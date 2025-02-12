using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DofusData.OauthData;
using OauthService.Services;
using Google.Apis.Auth;

namespace OauthService.Controller;


[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var existingUser = await _userService.GetUserByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return BadRequest("L'utilisateur existe déjà.");
        }

        var user = new User { FullName = model.FullName, Email = model.Email, PasswordHash = model.Password };
        await _userService.CreateUserAsync(user);

        return Ok(new { message = "Inscription réussie !" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userService.GetUserByEmailAsync(model.Email);
        if (user == null || !_userService.VerifyPassword(model.Password, user.PasswordHash))
        {
            return Unauthorized("Email ou mot de passe incorrect.");
        }

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequest request)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, 
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["GoogleOAuth:ClientId"] }
                });

            if (payload == null)
                return Unauthorized("Token Google invalide.");

            // Vérifie si l'utilisateur existe dans la base MongoDB
            var user = await _userService.GetUserByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new User
                {
                    FullName = payload.Name,
                    Email = payload.Email,
                    Provider = "google"
                };
                await _userService.CreateUserAsync(user);
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest($"Erreur de connexion Google: {ex.Message}");
        }
    }
}