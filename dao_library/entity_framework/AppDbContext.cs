using Microsoft.EntityFrameworkCore;
using entity_library;

namespace dao_library.entity_framework;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions options) : base(options) { } // No tiene que ir nada dentro de las llaves

	public DbSet<User> Users { get; set; } // Aca colocamos la entidad que queremos guardar en la base de datos (se debe hacer con c/u de ellas)
}
