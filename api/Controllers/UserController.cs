namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserDAO _userDAO;

    public UserController(ILogger<UserController> logger, UserDAO userDAO)
    {
        _logger = logger;
        _userDAO = userDAO;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        return Ok(await _userDAO.GetAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(long id)
    {
        User? user = await _userDAO.ReadUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<User>> GetByEmail(string email)
    {
        User? user = await _userDAO.GetUserEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (await _userDAO.GetUserEmailAsync(user.Email) != null)
        {
            return BadRequest("User with the same email already exists.");
        }

        User createdUser = await _userDAO.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(long id, User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? existingUser = await _userDAO.ReadUserByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        user.Id = id;
        await _userDAO.UpdateUserAsync(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        bool deleted = await _userDAO.DeleteUserByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
