namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class PersonDAO
{
    private readonly AppDbContext _db;

    public PersonDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        _db.People.Add(person);
        await _db.SaveChangesAsync();
        return person;
    }

    public Task<Person?> ReadPersonByIdAsync(long id)
    {
        return _db.People.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> UpdatePersonAsync(Person person)
    {
        Person? existing = await _db.People.FindAsync(person.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = person.Name;
        existing.Age = person.Age;
        existing.Dni = person.Dni;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePersonByIdAsync(long id)
    {
        Person? existing = await _db.People.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.People.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Person>> GetAllPeopleAsync()
    {
        return _db.People.AsNoTracking().ToListAsync();
    }
}
