namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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
    public ActionResult<List<User>> GetAll()
    {
        return Ok(_userDAO.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(long id)
    {
        User? user = _userDAO.ReadUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public ActionResult<User> GetByEmail(string email)
    {
        User? user = _userDAO.GetUserEmail(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (_userDAO.GetUserEmail(user.Email) != null)
        {
            return BadRequest("User with the same email already exists.");
        }

        User createdUser = _userDAO.CreateUser(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(long id, User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? existingUser = _userDAO.ReadUserById(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        user.Id = id;
        _userDAO.UpdateUser(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(long id)
    {
        bool deleted = _userDAO.DeleteUserById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
