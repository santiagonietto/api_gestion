namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class StudentDAO
{
    private readonly AppDbContext _db;

    public StudentDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        await AttachCoursesAsync(student);
        _db.Students.Add(student);
        await _db.SaveChangesAsync();
        return student;
    }

    public Task<Student?> ReadStudentByIdAsync(long id)
    {
        return _db.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> UpdateStudentAsync(Student student)
    {
        Student? existing = await _db.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == student.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = student.Name;
        existing.Age = student.Age;
        existing.Dni = student.Dni;
        existing.File = student.File;
        await SyncCoursesAsync(existing, student.Courses.Select(c => c.Id).ToList());
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteStudentByIdAsync(long id)
    {
        Student? existing = await _db.Students.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Students.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Student>> GetAllStudentsAsync()
    {
        return _db.Students.Include(s => s.Courses).AsNoTracking().ToListAsync();
    }

    private async Task AttachCoursesAsync(Student student)
    {
        if (student.Courses.Count == 0) return;
        var ids = student.Courses.Select(c => c.Id).Where(id => id != 0).ToList();
        if (ids.Count == 0) return;
        var attached = await _db.Courses.Where(c => ids.Contains(c.Id)).ToListAsync();
        student.Courses = attached.Concat(student.Courses.Where(c => c.Id == 0)).ToList();
    }

    private async Task SyncCoursesAsync(Student existing, List<long> courseIds)
    {
        var courses = await _db.Courses.Where(c => courseIds.Contains(c.Id)).ToListAsync();
        existing.Courses.Clear();
        foreach (var course in courses)
        {
            existing.Courses.Add(course);
        }
    }
}
