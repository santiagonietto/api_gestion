namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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
    public ActionResult<List<Course>> GetAll()
    {
        return Ok(_courseDAO.GetAllCourses());
    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetById(long id)
    {
        Course? course = _courseDAO.ReadCourseById(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpPost]
    public ActionResult<Course> CreateCourse(Course course)
    {
        Course createdCourse = _courseDAO.CreateCourse(course);
        return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
    }

    [HttpPut("{id}")]
    public ActionResult<Course> UpdateCourse(long id, Course course)
    {
        course.Id = id;
        bool updated = _courseDAO.UpdateCourse(course);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(long id)
    {
        bool deleted = _courseDAO.DeleteCourseById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
