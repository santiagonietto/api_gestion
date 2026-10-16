namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class TrainerDAO
{
    private readonly AppDbContext _db;

    public TrainerDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Trainer> CreateTrainerAsync(Trainer trainer)
    {
        _db.Trainers.Add(trainer);
        await _db.SaveChangesAsync();
        return trainer;
    }

    public Task<Trainer?> ReadTrainerByIdAsync(long id)
    {
        return _db.Trainers.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> UpdateTrainerAsync(Trainer trainer)
    {
        Trainer? existing = await _db.Trainers.FindAsync(trainer.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = trainer.Name;
        existing.Age = trainer.Age;
        existing.Dni = trainer.Dni;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTrainerByIdAsync(long id)
    {
        Trainer? existing = await _db.Trainers.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Trainers.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Trainer>> GetAllTrainersAsync()
    {
        return _db.Trainers.AsNoTracking().ToListAsync();
    }
}
