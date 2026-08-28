namespace dao_library;
using entity_library;

public class ActivityDAO
{
    public Activity CreateActivity(Activity activity)
    {
        activity.Id = MockDatabase.Activities.Count > 0
            ? MockDatabase.Activities.Max(a => a.Id) + 1
            : 1;
        MockDatabase.Activities.Add(activity);
        return activity;
    }

    public Activity? ReadActivityById(long id)
    {
        return MockDatabase.Activities.FirstOrDefault(a => a.Id == id);
    }

    public bool UpdateActivity(Activity activity)
    {
        Activity? existing = ReadActivityById(activity.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Title = activity.Title;
        existing.Description = activity.Description;
        existing.Date = activity.Date;
        existing.TypeActivity = activity.TypeActivity;
        return true;
    }

    public bool DeleteActivityById(long id)
    {
        Activity? existing = ReadActivityById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Activities.Remove(existing);
        return true;
    }

    public List<Activity> GetAllActivities()
    {
        return MockDatabase.Activities;
    }
}
