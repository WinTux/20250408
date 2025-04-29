using AutoMapper;
using Campus.DTO;
using Campus.Models;
using Campus.Repositories;
using System.Text.Json;

namespace Campus.Eventos
{
    public class ProcesadorDeEventos : IProcessadorDeEventos
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public ProcesadorDeEventos(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }
        public void ProcesarEvento(string tipo)
        {
            var tipoRes = DeterminarEvento(tipo);
            switch(tipoRes)
            {
                case TipoDeEventos.estudiante_publicado:
                    agregarEstudiante(tipo);
                    break;
                default:
                    Console.WriteLine($"Evento desconocido: {tipo}");
                    break;
            }
        }

        private TipoDeEventos DeterminarEvento(string tipo)
        {
            EventoDTO eventoDTO = JsonSerializer.Deserialize<EventoDTO>(tipo);
            switch (eventoDTO.evento)
            {
                case "estudiante_publicado":
                    return TipoDeEventos.estudiante_publicado;
                default:
                    return TipoDeEventos.desconocido;
            }
        }
        private void agregarEstudiante(string mensajeEstudiantePublisher)
        {
            using (var alcance = _scopeFactory.CreateScope())
            {
                var repo = alcance.ServiceProvider.GetRequiredService<IPerfilRepository>();
                var estudiantePublisherDTO = JsonSerializer.Deserialize<EstudiantePublisherDTO>(mensajeEstudiantePublisher);
                try
                {
                    var est = _mapper.Map<Estudiante>(estudiantePublisherDTO);
                    if (!repo.ExisteEstudianteForaneo(est.fci))
                    {
                        repo.CrearEstudiante(est);
                        repo.Guardar();
                    }
                    else
                    {
                        Console.WriteLine($"El estudiante {est.fci} ya existe en la base de datos.");
                    }
                }
                catch (Exception e) { 
                    Console.WriteLine($"Error al agregar el estudiante: {e.Message}");
                }
            }
        }
    }
    enum TipoDeEventos{
        estudiante_publicado,
        desconocido
    }
}
