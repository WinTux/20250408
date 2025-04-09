using Plataforma.Models;

namespace Plataforma.Repositories
{
    public class ImplEstudianteRepository : IEstudianteRepository
    {
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            var estudiantes = new List<Estudiante>
            {
                new Estudiante() {
                    id = 1,
                    nombre = "Pepe",
                    apellido = "Perales",
                    correo = "pperales@gmail.com",
                    telefono = 123456789
                },
                new Estudiante() {
                    id = 2,
                    nombre = "Ana",
                    apellido = "Sosa",
                    correo = "anita@gmail.com",
                    telefono = 123456789
                },
                new Estudiante() {
                    id = 3,
                    nombre = "Sofía",
                    apellido = "Pereira",
                    correo = "sp_ra@gmail.com",
                    telefono = 123456789
                }
            };
            return estudiantes;
        }
        public Estudiante GetEstudianteById(int id)
        {
            return new Estudiante()
            {
                id = 2,
                nombre = "Ana",
                apellido = "Sosa",
                correo = "anita@gmail.com",
                telefono = 123456789
            };
        }
    }
}
