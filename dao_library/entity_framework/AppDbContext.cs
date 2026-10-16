using Microsoft.EntityFrameworkCore;
using entity_library;

namespace dao_library.entity_framework;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Herencia TPH: Person es la base de Player, Student, Trainer y User.
    // EF crea una sola tabla People con columna Discriminator.
    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Trainer> Trainers { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Activity> Activities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPH explícito para la jerarquía Person
        modelBuilder.Entity<Person>().UseTphMappingStrategy();

        // Email único en Users
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // N:M Player <-> Team (EF crea la tabla puente automáticamente)
        modelBuilder.Entity<Player>()
            .HasMany(p => p.Teams)
            .WithMany(t => t.Players);

        // N:M Student <-> Course
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students);

        // 1:N Course -> Activities
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Activities)
            .WithOne(a => a.Course)
            .HasForeignKey(a => a.CourseId)
            .OnDelete(DeleteBehavior.SetNull);

        // Usuario default de la aplicación: Name=User, Password=Password123 (hash BCrypt)
        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = 1L,
                Name = "User",
                Age = 30,
                Dni = "00000000",
                Email = "user@gestion.local",
                Password = "$2b$11$SLXCxO7wO6ks7qkw/98LK.nZCzPSfmOpkApCYDf4xsdLAuSS2peBu"
            }
        );
    }
}
