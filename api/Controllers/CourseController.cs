namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CourseController : ControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly CourseDAO _courseDAO;

    public CourseController(ILogger<CourseController> logger, CourseDAO courseDAO)
    {
        _logger = logger;
        _courseDAO = courseDAO;
    }

    [HttpGet]
    public async Task<ActionResult<List<Course>>> GetAll()
    {
        return Ok(await _courseDAO.GetAllCoursesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetById(long id)
    {
        Course? course = await _courseDAO.ReadCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        Course createdCourse = await _courseDAO.CreateCourseAsync(course);
        return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Course>> UpdateCourse(long id, Course course)
    {
        course.Id = id;
        bool updated = await _courseDAO.UpdateCourseAsync(course);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(long id)
    {
        bool deleted = await _courseDAO.DeleteCourseByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
