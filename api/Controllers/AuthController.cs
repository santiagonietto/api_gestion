namespace api.Controllers;

using api.DTOs;
using api.Services;
using dao_library;
using entity_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserDAO _userDAO;
    private readonly TokenService _tokenService;

    public AuthController(UserDAO userDAO, TokenService tokenService)
    {
        _userDAO = userDAO;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Firma del login: recibe email + password, verifica el hash BCrypt
    /// y devuelve el JWT firmado más los datos básicos del usuario.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? user = await _userDAO.GetUserEmailAsync(request.Email);
        if (user == null || !PasswordHasher.Verify(request.Password, user.Password))
        {
            return Unauthorized("Invalid email or password.");
        }

        var (token, expiresAt) = _tokenService.CreateToken(user);

        return Ok(new AuthResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }
}
