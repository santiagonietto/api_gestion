namespace entity_library;

public class Student : Person
{
    private int file = 0;
    private List<Course> courses = new();

    public int File
    {
        get { return file; }
        set { file = value; }
    }

    public List<Course> Courses
    {
        get { return courses; }
        set { courses = value; }
    }

    public void AddCourse(Course course)
    {
        courses.Add(course);
    }
}
