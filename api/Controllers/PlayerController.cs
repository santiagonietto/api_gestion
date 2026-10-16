namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    public async Task<ActionResult<List<Player>>> GetAll()
    {
        return Ok(await _playerDAO.GetAllPlayersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetById(long id)
    {
        Player? player = await _playerDAO.ReadPlayerByIdAsync(id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpGet("byteam/{teamId}")]
    public async Task<ActionResult<List<Player>>> GetByTeam(long teamId)
    {
        return Ok(await _playerDAO.GetPlayersByTeamAsync(teamId));
    }

    [HttpPost]
    public async Task<ActionResult<Player>> CreatePlayer(Player player)
    {
        Player createdPlayer = await _playerDAO.CreatePlayerAsync(player);
        return CreatedAtAction(nameof(GetById), new { id = createdPlayer.Id }, createdPlayer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Player>> UpdatePlayer(long id, Player player)
    {
        player.Id = id;
        bool updated = await _playerDAO.UpdatePlayerAsync(player);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(long id)
    {
        bool deleted = await _playerDAO.DeletePlayerByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
