using Plataforma.Models;

namespace Plataforma.Repositories
{
    public class ImplEstudianteRepository : IEstudianteRepository
    {
        private readonly InstitutoDbContext _context;
        public ImplEstudianteRepository(InstitutoDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return _context.estudiantes.ToList(); // Cargar todos los estudiantes desde la base de datos
            /*var estudiantes = new List<Estudiante>
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
            return estudiantes;*/
        }
        public Estudiante GetEstudianteById(int id)
        {
            return _context.estudiantes.FirstOrDefault(e => e.id == id); // Cargar un estudiante por su ID desde la base de datos
            /*return new Estudiante()
            {
                id = 2,
                nombre = "Ana",
                apellido = "Sosa",
                correo = "anita@gmail.com",
                telefono = 123456789
            };*/
        }

        public void CreateEstudiante(Estudiante estudiante)
        {
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            _context.estudiantes.Add(estudiante); // Agregar un nuevo estudiante a la base de datos
        }
        public bool UpdateEstudiante(Estudiante estudiante)
        {
            /*
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            _context.estudiantes.Update(estudiante); // Actualizar un estudiante existente en la base de datos
            */
            return true;
        }
        public void DeleteEstudiante(Estudiante estudiante)
        {
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            _context.estudiantes.Remove(estudiante); // Eliminar un estudiante de la base de datos
        }
        public bool GuardarCambios()
        {
            return (_context.SaveChanges() >= 0); // Guardar los cambios en la base de datos
        }
    }
}
