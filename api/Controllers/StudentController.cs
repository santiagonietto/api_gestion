namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        return Ok(await _studentDAO.GetAllStudentsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(long id)
    {
        Student? student = await _studentDAO.ReadStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        Student createdStudent = await _studentDAO.CreateStudentAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> UpdateStudent(long id, Student student)
    {
        student.Id = id;
        bool updated = await _studentDAO.UpdateStudentAsync(student);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(long id)
    {
        bool deleted = await _studentDAO.DeleteStudentByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
