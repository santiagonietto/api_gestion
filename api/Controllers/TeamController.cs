namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly TeamDAO _teamDAO;

    public TeamController(ILogger<TeamController> logger, TeamDAO teamDAO)
    {
        _logger = logger;
        _teamDAO = teamDAO;
    }

    [HttpGet]
    public async Task<ActionResult<List<Team>>> GetAll()
    {
        return Ok(await _teamDAO.GetAllTeamsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetById(long id)
    {
        Team? team = await _teamDAO.ReadTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(Team team)
    {
        Team createdTeam = await _teamDAO.CreateTeamAsync(team);
        return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Team>> UpdateTeam(long id, Team team)
    {
        team.Id = id;
        bool updated = await _teamDAO.UpdateTeamAsync(team);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(long id)
    {
        bool deleted = await _teamDAO.DeleteTeamByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
