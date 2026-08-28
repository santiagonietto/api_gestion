namespace dao_library;
using entity_library;

public class UserDAO
{
    public User CreateUser(User user)
    {
        user.Id = MockDatabase.Users.Count > 0
            ? MockDatabase.Users.Max(u => u.Id) + 1
            : 1;
        MockDatabase.Users.Add(user);
        return user;
    }

    public User? ReadUserById(long id)
    {
        return MockDatabase.Users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetUserEmail(string email)
    {
        return MockDatabase.Users.FirstOrDefault(u => u.Email == email);
    }

    public bool UpdateUser(User user)
    {
        User? existing = ReadUserById(user.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = user.Name;
        existing.Dni = user.Dni;
        existing.Email = user.Email;
        existing.Password = user.Password;
        return true;
    }

    public bool DeleteUserById(long id)
    {
        User? existing = ReadUserById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Users.Remove(existing);
        return true;
    }

    public List<User> GetAllUsers()
    {
        return MockDatabase.Users;
    }
}
