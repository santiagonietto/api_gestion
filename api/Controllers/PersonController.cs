namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly PersonDAO _personDAO;

    public PersonController(ILogger<PersonController> logger, PersonDAO personDAO)
    {
        _logger = logger;
        _personDAO = personDAO;
    }

    [HttpGet]
    public ActionResult<List<Person>> GetAll()
    {
        return Ok(_personDAO.GetAllPeople());
    }

    [HttpGet("{id}")]
    public ActionResult<Person> GetById(long id)
    {
        Person? person = _personDAO.ReadPersonById(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public ActionResult<Person> CreatePerson(Person person)
    {
        Person createdPerson = _personDAO.CreatePerson(person);
        return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("{id}")]
    public ActionResult<Person> UpdatePerson(long id, Person person)
    {
        person.Id = id;
        bool updated = _personDAO.UpdatePerson(person);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePerson(long id)
    {
        bool deleted = _personDAO.DeletePersonById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
