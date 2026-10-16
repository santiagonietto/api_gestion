namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class UserDAO
{
    private readonly AppDbContext _db;

    public UserDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // Nunca se guarda la contraseña en plano: se hashea con BCrypt.
        if (!PasswordHasher.IsHashed(user.Password))
        {
            user.Password = PasswordHasher.Hash(user.Password);
        }
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public Task<User?> ReadUserByIdAsync(long id)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetUserEmailAsync(string email)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        User? existing = await _db.Users.FindAsync(user.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = user.Name;
        existing.Age = user.Age;
        existing.Dni = user.Dni;
        existing.Email = user.Email;
        existing.Password = PasswordHasher.IsHashed(user.Password)
            ? user.Password
            : PasswordHasher.Hash(user.Password);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserByIdAsync(long id)
    {
        User? existing = await _db.Users.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Users.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        return _db.Users.AsNoTracking().ToListAsync();
    }
}
