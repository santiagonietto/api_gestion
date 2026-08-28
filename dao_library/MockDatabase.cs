namespace dao_library;
using entity_library;

public static class MockDatabase
{
    public static List<Person> People { get; set; } = new();
    public static List<Trainer> Trainers { get; set; } = new();
    public static List<Player> Players { get; set; } = new();
    public static List<Team> Teams { get; set; } = new();
    public static List<Student> Students { get; set; } = new();
    public static List<Course> Courses { get; set; } = new();
    public static List<Activity> Activities { get; set; } = new();
    public static List<User> Users { get; set; } = new()
    {
        new User
        {
            Id = 1,
            Name = "Pedro",
            Dni = "1234",
            Email = "pedro@email.com",
            Password = "1234"
        },
        new User
        {
            Id = 2,
            Name = "Ana",
            Dni = "5678",
            Email = "ana@email.com",
            Password = "abcd"
        }
    };

    public static void Seed()
    {
        if (Trainers.Count > 0)
        {
            return;
        }

        Trainer trainer1 = new Trainer { Id = 1, Name = "Carlos", Age = 40, Dni = "11111" };
        Trainer trainer2 = new Trainer { Id = 2, Name = "Lucia", Age = 35, Dni = "22222" };
        Trainers.Add(trainer1);
        Trainers.Add(trainer2);

        Player player1 = new Player { Id = 1, Name = "Juan", Age = 20, Dni = "33333", Numero = 7 };
        Player player2 = new Player { Id = 2, Name = "Maria", Age = 21, Dni = "44444", Numero = 10 };
        Player player3 = new Player { Id = 3, Name = "Sofia", Age = 22, Dni = "55555", Numero = 9 };
        Players.Add(player1);
        Players.Add(player2);
        Players.Add(player3);

        Team team1 = new Team { Id = 1, Name = "Los Leones", Category = "Primera" };
        Team team2 = new Team { Id = 2, Name = "Las Aguilas", Category = "Segunda" };
        Teams.Add(team1);
        Teams.Add(team2);

        team1.AddPlayer(player1);
        team1.AddPlayer(player2);
        player1.AddTeam(team1);
        player2.AddTeam(team1);

        team2.AddPlayer(player3);
        player3.AddTeam(team2);

        Student student1 = new Student { Id = 1, Name = "Marta", Age = 19, Dni = "66666", File = 1001 };
        Student student2 = new Student { Id = 2, Name = "Luis", Age = 20, Dni = "77777", File = 1002 };
        Students.Add(student1);
        Students.Add(student2);

        Course course1 = new Course { Id = 1, Name = "Matematica" };
        Course course2 = new Course { Id = 2, Name = "Programacion" };
        Courses.Add(course1);
        Courses.Add(course2);

        course1.AddStudent(student1);
        course1.AddStudent(student2);
        student1.AddCourse(course1);
        student2.AddCourse(course1);

        course2.AddStudent(student1);
        student1.AddCourse(course2);

        Activity activity1 = new Activity
        {
            Id = 1,
            Title = "TP1",
            Description = "Primer trabajo practico",
            Date = new DateTime(2026, 9, 1),
            TypeActivity = TypeActivity.PracticalWork
        };
        Activity activity2 = new Activity
        {
            Id = 2,
            Title = "Parcial 1",
            Date = new DateTime(2026, 10, 5),
            TypeActivity = TypeActivity.Exam
        };
        Activities.Add(activity1);
        Activities.Add(activity2);

        course1.AddActivity(activity1);
        course1.AddActivity(activity2);
    }
}
