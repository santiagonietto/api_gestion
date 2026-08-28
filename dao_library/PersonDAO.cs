namespace dao_library;
using entity_library;

public class PersonDAO
{
    public Person CreatePerson(Person person)
    {
        person.Id = MockDatabase.People.Count > 0
            ? MockDatabase.People.Max(p => p.Id) + 1
            : 1;
        MockDatabase.People.Add(person);
        return person;
    }

    public Person? ReadPersonById(long id)
    {
        return MockDatabase.People.FirstOrDefault(p => p.Id == id);
    }

    public bool UpdatePerson(Person person)
    {
        Person? existing = ReadPersonById(person.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = person.Name;
        existing.Age = person.Age;
        existing.Dni = person.Dni;
        return true;
    }

    public bool DeletePersonById(long id)
    {
        Person? existing = ReadPersonById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.People.Remove(existing);
        return true;
    }

    public List<Person> GetAllPeople()
    {
        return MockDatabase.People;
    }
}
