using Plataforma.Models;

namespace Plataforma.Repositories
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteById(int id);
        
    }
}
