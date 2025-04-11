using Microsoft.EntityFrameworkCore;
using Plataforma.Models;

namespace Plataforma.Repositories
{
    public class InstitutoDbContext : DbContext
    {
        public DbSet<Estudiante> estudiantes { get; set; }
        public InstitutoDbContext(DbContextOptions<InstitutoDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<Estudiante>().HasKey(e => e.id);
            modelBuilder.Entity<Estudiante>().Property(e => e.nombre).IsRequired();
            modelBuilder.Entity<Estudiante>().Property(e => e.apellido).IsRequired();
            modelBuilder.Entity<Estudiante>().Property(e => e.correo).IsRequired();
            modelBuilder.Entity<Estudiante>().Property(e => e.telefono);
        }
    }
}
