namespace dao_library;
using entity_library;

public class StudentDAO
{
    public Student CreateStudent(Student student)
    {
        student.Id = MockDatabase.Students.Count > 0
            ? MockDatabase.Students.Max(s => s.Id) + 1
            : 1;
        MockDatabase.Students.Add(student);
        return student;
    }

    public Student? ReadStudentById(long id)
    {
        return MockDatabase.Students.FirstOrDefault(s => s.Id == id);
    }

    public bool UpdateStudent(Student student)
    {
        Student? existing = ReadStudentById(student.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = student.Name;
        existing.Age = student.Age;
        existing.Dni = student.Dni;
        existing.File = student.File;
        return true;
    }

    public bool DeleteStudentById(long id)
    {
        Student? existing = ReadStudentById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Students.Remove(existing);
        return true;
    }

    public List<Student> GetAllStudents()
    {
        return MockDatabase.Students;
    }
}
