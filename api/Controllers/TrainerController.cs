namespace api.Controllers;
using entity_library;
using dao_library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    public async Task<ActionResult<List<Trainer>>> GetAll()
    {
        return Ok(await _trainerDAO.GetAllTrainersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trainer>> GetById(long id)
    {
        Trainer? trainer = await _trainerDAO.ReadTrainerByIdAsync(id);
        if (trainer == null)
        {
            return NotFound();
        }
        return Ok(trainer);
    }

    [HttpPost]
    public async Task<ActionResult<Trainer>> CreateTrainer(Trainer trainer)
    {
        Trainer createdTrainer = await _trainerDAO.CreateTrainerAsync(trainer);
        return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Trainer>> UpdateTrainer(long id, Trainer trainer)
    {
        trainer.Id = id;
        bool updated = await _trainerDAO.UpdateTrainerAsync(trainer);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(trainer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrainer(long id)
    {
        bool deleted = await _trainerDAO.DeleteTrainerByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
