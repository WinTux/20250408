using Plataforma.DTO;

namespace Plataforma.ComunicacionSync.Http
{
    public interface ICampusHistorialCliente
    {
        Task ComunicarseConCampus(EstudianteReadDTO estudiante);
        // Crear implementación
        //http client
        // Este ejemplo es para comunicación sincrónico.
    }
}
