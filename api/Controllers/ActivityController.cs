namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ActivityController : ControllerBase
{
    private readonly ILogger<ActivityController> _logger;
    private readonly ActivityDAO _activityDAO;

    public ActivityController(ILogger<ActivityController> logger, ActivityDAO activityDAO)
    {
        _logger = logger;
        _activityDAO = activityDAO;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetAll()
    {
        return Ok(await _activityDAO.GetAllActivitiesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetById(long id)
    {
        Activity? activity = await _activityDAO.ReadActivityByIdAsync(id);
        if (activity == null)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    [HttpPost]
    public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
    {
        Activity createdActivity = await _activityDAO.CreateActivityAsync(activity);
        return CreatedAtAction(nameof(GetById), new { id = createdActivity.Id }, createdActivity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Activity>> UpdateActivity(long id, Activity activity)
    {
        activity.Id = id;
        bool updated = await _activityDAO.UpdateActivityAsync(activity);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(long id)
    {
        bool deleted = await _activityDAO.DeleteActivityByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
