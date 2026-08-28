namespace entity_library;

public class Course
{
    private long id = 0;
    private string name = "";
    private List<Student> students = new();
    private List<Activity> activities = new();

    public long Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public List<Student> Students
    {
        get { return students; }
        set { students = value; }
    }

    public List<Activity> Activities
    {
        get { return activities; }
        set { activities = value; }
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public void AddActivity(Activity activity)
    {
        activities.Add(activity);
    }
}
