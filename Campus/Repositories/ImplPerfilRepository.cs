using Campus.Models;

namespace Campus.Repositories
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            throw new NotImplementedException();
        }
        public void CrearEstudiante(Estudiante estudiante)
        {
            throw new NotImplementedException();
        }
        public bool ExisteEstudiante(int ci)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci)
        {
            throw new NotImplementedException();
        }
        public Perfil GetPerfil(int idPerfil, int ci)
        {
            throw new NotImplementedException();
        }

        public void CrearPerfil(int ci, Perfil perfil)
        {
            throw new NotImplementedException();
        }
        public bool Guardar()
        {
            throw new NotImplementedException();
        }
    }
}
