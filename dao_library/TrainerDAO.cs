namespace dao_library;
using entity_library;

public class TrainerDAO
{
    public Trainer CreateTrainer(Trainer trainer)
    {
        trainer.Id = MockDatabase.Trainers.Count > 0
            ? MockDatabase.Trainers.Max(t => t.Id) + 1
            : 1;
        MockDatabase.Trainers.Add(trainer);
        return trainer;
    }

    public Trainer? ReadTrainerById(long id)
    {
        return MockDatabase.Trainers.FirstOrDefault(t => t.Id == id);
    }

    public bool UpdateTrainer(Trainer trainer)
    {
        Trainer? existing = ReadTrainerById(trainer.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = trainer.Name;
        existing.Age = trainer.Age;
        existing.Dni = trainer.Dni;
        return true;
    }

    public bool DeleteTrainerById(long id)
    {
        Trainer? existing = ReadTrainerById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Trainers.Remove(existing);
        return true;
    }

    public List<Trainer> GetAllTrainers()
    {
        return MockDatabase.Trainers;
    }
}
