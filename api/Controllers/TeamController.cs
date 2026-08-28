namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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
    public ActionResult<List<Team>> GetAll()
    {
        return Ok(_teamDAO.GetAllTeams());
    }

    [HttpGet("{id}")]
    public ActionResult<Team> GetById(long id)
    {
        Team? team = _teamDAO.ReadTeamById(id);
        if (team == null)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpPost]
    public ActionResult<Team> CreateTeam(Team team)
    {
        Team createdTeam = _teamDAO.CreateTeam(team);
        return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
    }

    [HttpPut("{id}")]
    public ActionResult<Team> UpdateTeam(long id, Team team)
    {
        team.Id = id;
        bool updated = _teamDAO.UpdateTeam(team);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeam(long id)
    {
        bool deleted = _teamDAO.DeleteTeamById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
