using Plataforma.DTO;

namespace Plataforma.ComunicacionAsync
{
    public interface IBusDeMensajesCliente
    {
        void PublicarNuevoEstudiante(EstudiantePublisherDTO estudiantePublisherDTO);
    }
}
