namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TrainerController : ControllerBase
{
    private readonly ILogger<TrainerController> _logger;
    private readonly TrainerDAO _trainerDAO;

    public TrainerController(ILogger<TrainerController> logger, TrainerDAO trainerDAO)
    {
        _logger = logger;
        _trainerDAO = trainerDAO;
    }

    [HttpGet]
    public ActionResult<List<Trainer>> GetAll()
    {
        return Ok(_trainerDAO.GetAllTrainers());
    }

    [HttpGet("{id}")]
    public ActionResult<Trainer> GetById(long id)
    {
        Trainer? trainer = _trainerDAO.ReadTrainerById(id);
        if (trainer == null)
        {
            return NotFound();
        }
        return Ok(trainer);
    }

    [HttpPost]
    public ActionResult<Trainer> CreateTrainer(Trainer trainer)
    {
        Trainer createdTrainer = _trainerDAO.CreateTrainer(trainer);
        return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
    }

    [HttpPut("{id}")]
    public ActionResult<Trainer> UpdateTrainer(long id, Trainer trainer)
    {
        trainer.Id = id;
        bool updated = _trainerDAO.UpdateTrainer(trainer);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(trainer);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTrainer(long id)
    {
        bool deleted = _trainerDAO.DeleteTrainerById(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
