namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    public async Task<ActionResult<List<Person>>> GetAll()
    {
        return Ok(await _personDAO.GetAllPeopleAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetById(long id)
    {
        Person? person = await _personDAO.ReadPersonByIdAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson(Person person)
    {
        Person createdPerson = await _personDAO.CreatePersonAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Person>> UpdatePerson(long id, Person person)
    {
        person.Id = id;
        bool updated = await _personDAO.UpdatePersonAsync(person);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(long id)
    {
        bool deleted = await _personDAO.DeletePersonByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
