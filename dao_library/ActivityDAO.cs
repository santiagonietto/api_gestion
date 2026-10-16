namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class ActivityDAO
{
    private readonly AppDbContext _db;

    public ActivityDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Activity> CreateActivityAsync(Activity activity)
    {
        _db.Activities.Add(activity);
        await _db.SaveChangesAsync();
        return activity;
    }

    public Task<Activity?> ReadActivityByIdAsync(long id)
    {
        return _db.Activities.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> UpdateActivityAsync(Activity activity)
    {
        Activity? existing = await _db.Activities.FindAsync(activity.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Title = activity.Title;
        existing.Description = activity.Description;
        existing.Date = activity.Date;
        existing.TypeActivity = activity.TypeActivity;
        existing.CourseId = activity.CourseId;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteActivityByIdAsync(long id)
    {
        Activity? existing = await _db.Activities.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Activities.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Activity>> GetAllActivitiesAsync()
    {
        return _db.Activities.AsNoTracking().ToListAsync();
    }
}
