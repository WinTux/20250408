using Campus.Models;
using Microsoft.EntityFrameworkCore;

namespace Campus.Conexion
{
    public class CampusDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public CampusDbContext(DbContextOptions<CampusDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // [Estudiante] 1:N [Perfil]
            modelBuilder.Entity<Estudiante>()
                .HasMany(est => est.perfiles)
                .WithOne(p => p.estudiante)
                .HasForeignKey(p => p.estudianteCI);
            modelBuilder.Entity<Perfil>()
                .HasOne(p => p.estudiante)
                .WithMany(est => est.perfiles)
                .HasForeignKey(p => p.estudianteCI);
        }
    }
}
