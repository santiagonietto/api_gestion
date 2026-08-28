namespace dao_library;
using entity_library;

public class CourseDAO
{
    public Course CreateCourse(Course course)
    {
        course.Id = MockDatabase.Courses.Count > 0
            ? MockDatabase.Courses.Max(c => c.Id) + 1
            : 1;
        MockDatabase.Courses.Add(course);
        return course;
    }

    public Course? ReadCourseById(long id)
    {
        return MockDatabase.Courses.FirstOrDefault(c => c.Id == id);
    }

    public bool UpdateCourse(Course course)
    {
        Course? existing = ReadCourseById(course.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = course.Name;
        return true;
    }

    public bool DeleteCourseById(long id)
    {
        Course? existing = ReadCourseById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Courses.Remove(existing);
        return true;
    }

    public List<Course> GetAllCourses()
    {
        return MockDatabase.Courses;
    }
}
