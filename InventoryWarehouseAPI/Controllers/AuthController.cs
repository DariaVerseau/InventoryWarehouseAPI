using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InventoryWarehouseAPI.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(
        AppDbContext context,
        IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Проверка на существующего пользователя
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            return BadRequest("Пользователь с таким email уже существует");

        // Создание пользователя
        var user = new User
        {
            Email = model.Email,
            PasswordHash = HashPassword(model.Password) // Реализуйте хэширование
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Пользователь успешно зарегистрирован");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            return Unauthorized("Неверный email или пароль");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            // Добавьте другие claims по необходимости
            // new(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        // Реализуйте хэширование пароля (например, с помощью BCrypt)
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        // Проверка хэша пароля
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}