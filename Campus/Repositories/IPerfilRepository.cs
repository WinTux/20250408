using Campus.Models;

namespace Campus.Repositories
{
    public interface IPerfilRepository
    {
        // Para estudiantes
        IEnumerable<Estudiante> GetEstudiantes();
        void CrearEstudiante(Estudiante estudiante);
        bool ExisteEstudiante(int ci);
        // Para perfiles
        IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci);
        Perfil GetPerfil(int idPerfil, int ci);
        void CrearPerfil(int ci, Perfil perfil);
        bool Guardar();

        bool ExisteEstudianteForaneo(int fci);
    }
}
