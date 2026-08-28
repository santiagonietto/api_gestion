namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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
    public ActionResult<List<Activity>> GetAll()
    {
        return Ok(_activityDAO.GetAllActivities());
    }

    [HttpGet("{id}")]
    public ActionResult<Activity> GetById(long id)
    {
        Activity? activity = _activityDAO.ReadActivityById(id);
        if (activity == null)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    [HttpPost]
    public ActionResult<Activity> CreateActivity(Activity activity)
    {
        Activity createdActivity = _activityDAO.CreateActivity(activity);
        return CreatedAtAction(nameof(GetById), new { id = createdActivity.Id }, createdActivity);
    }

    [HttpPut("{id}")]
    public ActionResult<Activity> UpdateActivity(long id, Activity activity)
    {
        activity.Id = id;
        bool updated = _activityDAO.UpdateActivity(activity);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteActivity(long id)
    {
        bool deleted = _activityDAO.DeleteActivityById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
