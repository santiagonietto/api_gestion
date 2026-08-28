namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentDAO _studentDAO;

    public StudentController(ILogger<StudentController> logger, StudentDAO studentDAO)
    {
        _logger = logger;
        _studentDAO = studentDAO;
    }

    [HttpGet]
    public ActionResult<List<Student>> GetAll()
    {
        return Ok(_studentDAO.GetAllStudents());
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(long id)
    {
        Student? student = _studentDAO.ReadStudentById(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> CreateStudent(Student student)
    {
        Student createdStudent = _studentDAO.CreateStudent(student);
        return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
    }

    [HttpPut("{id}")]
    public ActionResult<Student> UpdateStudent(long id, Student student)
    {
        student.Id = id;
        bool updated = _studentDAO.UpdateStudent(student);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(long id)
    {
        bool deleted = _studentDAO.DeleteStudentById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
