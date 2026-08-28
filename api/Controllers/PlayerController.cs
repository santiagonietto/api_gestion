namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly PlayerDAO _playerDAO;

    public PlayerController(ILogger<PlayerController> logger, PlayerDAO playerDAO)
    {
        _logger = logger;
        _playerDAO = playerDAO;
    }

    [HttpGet]
    public ActionResult<List<Player>> GetAll()
    {
        return Ok(_playerDAO.GetAllPlayers());
    }

    [HttpGet("{id}")]
    public ActionResult<Player> GetById(long id)
    {
        Player? player = _playerDAO.ReadPlayerById(id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpPost]
    public ActionResult<Player> CreatePlayer(Player player)
    {
        Player createdPlayer = _playerDAO.CreatePlayer(player);
        return CreatedAtAction(nameof(GetById), new { id = createdPlayer.Id }, createdPlayer);
    }

    [HttpPut("{id}")]
    public ActionResult<Player> UpdatePlayer(long id, Player player)
    {
        player.Id = id;
        bool updated = _playerDAO.UpdatePlayer(player);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePlayer(long id)
    {
        bool deleted = _playerDAO.DeletePlayerById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
