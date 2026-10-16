namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class CourseDAO
{
    private readonly AppDbContext _db;

    public CourseDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        await AttachRelationsAsync(course);
        _db.Courses.Add(course);
        await _db.SaveChangesAsync();
        return course;
    }

    public Task<Course?> ReadCourseByIdAsync(long id)
    {
        return _db.Courses
            .Include(c => c.Students)
            .Include(c => c.Activities)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> UpdateCourseAsync(Course course)
    {
        Course? existing = await _db.Courses
            .Include(c => c.Students)
            .Include(c => c.Activities)
            .FirstOrDefaultAsync(c => c.Id == course.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = course.Name;
        await SyncStudentsAsync(existing, course.Students.Select(s => s.Id).ToList());
        await SyncActivitiesAsync(existing, course.Activities.Select(a => a.Id).ToList());
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCourseByIdAsync(long id)
    {
        Course? existing = await _db.Courses.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Courses.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Course>> GetAllCoursesAsync()
    {
        return _db.Courses
            .Include(c => c.Students)
            .Include(c => c.Activities)
            .AsNoTracking()
            .ToListAsync();
    }

    private async Task AttachRelationsAsync(Course course)
    {
        if (course.Students.Count > 0)
        {
            var studentIds = course.Students.Select(s => s.Id).Where(id => id != 0).ToList();
            if (studentIds.Count > 0)
            {
                var attached = await _db.Students.Where(s => studentIds.Contains(s.Id)).ToListAsync();
                course.Students = attached.Concat(course.Students.Where(s => s.Id == 0)).ToList();
            }
        }
        if (course.Activities.Count > 0)
        {
            var activityIds = course.Activities.Select(a => a.Id).Where(id => id != 0).ToList();
            if (activityIds.Count > 0)
            {
                var attached = await _db.Activities.Where(a => activityIds.Contains(a.Id)).ToListAsync();
                course.Activities = attached.Concat(course.Activities.Where(a => a.Id == 0)).ToList();
            }
        }
    }

    private async Task SyncStudentsAsync(Course existing, List<long> studentIds)
    {
        var students = await _db.Students.Where(s => studentIds.Contains(s.Id)).ToListAsync();
        existing.Students.Clear();
        foreach (var student in students)
        {
            existing.Students.Add(student);
        }
    }

    private async Task SyncActivitiesAsync(Course existing, List<long> activityIds)
    {
        // Las activities son 1:N: se reasignan al course vía CourseId
        var activities = await _db.Activities.Where(a => activityIds.Contains(a.Id)).ToListAsync();
        existing.Activities.Clear();
        foreach (var activity in activities)
        {
            existing.Activities.Add(activity);
        }
    }
}
