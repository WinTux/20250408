using Plataforma.Models;

namespace Plataforma.Repositories
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteById(int id);
        void CreateEstudiante(Estudiante estudiante);
        bool UpdateEstudiante(Estudiante estudiante);
        void DeleteEstudiante(Estudiante estudiante);
        bool GuardarCambios();
    }
}
