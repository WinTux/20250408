using Campus.Conexion;
using Campus.Models;

namespace Campus.Repositories
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        private readonly CampusDbContext campusDbContext;
        public ImplPerfilRepository(CampusDbContext campusDbContext)
        {
            this.campusDbContext = campusDbContext;
        }
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return campusDbContext.Estudiantes.ToList();
        }
        public void CrearEstudiante(Estudiante estudiante)
        {
            if(estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            else
                campusDbContext.Estudiantes.Add(estudiante);
        }
        public bool ExisteEstudiante(int ci)
        {
            return campusDbContext.Estudiantes.Any(e => e.ci == ci);
        }
        public IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci)
        {
            return campusDbContext.Perfiles.Where(per => per.estudianteCI == ci).OrderBy(p=>p.nick).ToList();
        }
        public Perfil GetPerfil(int idPerfil, int ci)
        {
            return campusDbContext.Perfiles.Where(per=>per.id == idPerfil && per.estudianteCI == ci).FirstOrDefault();
        }

        public void CrearPerfil(int ci, Perfil perfil)
        {
            if(perfil == null)
                throw new ArgumentNullException(nameof(perfil));
            else
            {
                perfil.estudianteCI = ci;
                campusDbContext.Perfiles.Add(perfil);
            }
        }
        public bool Guardar()
        {
            return (campusDbContext.SaveChanges() >= 0);
        }
    }
}
