using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Plataforma.ComunicacionAsync;
using Plataforma.ComunicacionSync.Http;
using Plataforma.DTO;
using Plataforma.Models;
using Plataforma.Repositories;

namespace Plataforma.Controllers
{
    [Route("api/[controller]")] // localhost:5000/api/estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;
        private readonly ICampusHistorialCliente _campusHistorialCliente;
        private readonly IBusDeMensajesCliente _busDeMensajesCliente;

        public EstudianteController(IEstudianteRepository estudianteRepository, IMapper mapper, ICampusHistorialCliente campusHistorialCliente, IBusDeMensajesCliente busDeMensajesCliente)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
            _campusHistorialCliente = campusHistorialCliente;
            _busDeMensajesCliente = busDeMensajesCliente;
        }
        [HttpGet] // localhost:5000/api/estudiante [GET]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            var estudiantes = _estudianteRepository.GetEstudiantes();
            return Ok(_mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes)); // 200 OK
        }
        [HttpGet("{id}",Name = "GetEstudianteById")] // localhost:5000/api/estudiante/{id} [GET]
        public ActionResult<EstudianteReadDTO> GetEstudianteById(int id)
        {
            var estudiante = _estudianteRepository.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(_mapper.Map<EstudianteReadDTO>(estudiante)); // 200 OK
        }
        [HttpPost] // localhost:5000/api/estudiante [POST]
        public async Task<ActionResult<EstudianteReadDTO>> CreateEstudiante([FromBody] EstudianteCreateDTO estudianteCreateDTO)
        {
            if (estudianteCreateDTO == null)
            {
                return BadRequest(); // 400 Bad Request
            }
            var estudiante = _mapper.Map<Estudiante>(estudianteCreateDTO);
            _estudianteRepository.CreateEstudiante(estudiante);
            _estudianteRepository.GuardarCambios();
            var estudianteReadDTO = _mapper.Map<EstudianteReadDTO>(estudiante);
            #region Comunicación sync
            try
            {
                await _campusHistorialCliente.ComunicarseConCampus(estudianteReadDTO);
                Console.WriteLine("Se ha enviado el estudiante al servicio de Campus (por POST desde Plataforma, Desde EstudianteController)");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ocurrió un error al comunicarse con Campus de forma sincronizada: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al comunicarse con Campus de forma sincronizada (Excepción general): {ex.Message}");
            }
            #endregion

            #region Comunicación async
            try
            {
                var estudiantePublisherDTO = _mapper.Map<EstudiantePublisherDTO>(estudianteReadDTO);
                estudiantePublisherDTO.tipoEvento = "estudiante_publicado";
                _busDeMensajesCliente.PublicarNuevoEstudiante(estudiantePublisherDTO);
                Console.WriteLine("Se ha enviado el estudiante al servicio de Campus (por POST desde Plataforma, Desde EstudianteController) ASYNC");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al comunicarse con Campus de forma asíncrona: {ex.Message}");
            }
            #endregion
            return CreatedAtRoute(nameof(GetEstudianteById), new { id = estudianteReadDTO.id }, estudianteReadDTO); // 201 Created
        }
        [HttpPut("{codigo}")] // localhost:5000/api/estudiante/{id} [PUT]
        public ActionResult UpdateEstudiante(int codigo, [FromBody] EstudianteUpdateDTO estudianteUpdateDTO)
        {
            if (estudianteUpdateDTO == null)
            {
                return BadRequest(); // 400 Bad Request
            }
            var estudiante = _estudianteRepository.GetEstudianteById(codigo);
            if (estudiante == null)
            {
                return NotFound(); // 404 Not Found
            }
            _mapper.Map(estudianteUpdateDTO, estudiante);
            _estudianteRepository.UpdateEstudiante(estudiante);
            _estudianteRepository.GuardarCambios();
            return NoContent(); // 204 No Content
        }
        [HttpPatch("{id}")] // localhost:5000/api/estudiante/{id} [PATCH]
        public ActionResult PartialEstudianteUpdate(int id, JsonPatchDocument<EstudianteUpdateDTO> estPatch)
        {
            if (estPatch == null)
            {
                return BadRequest(); // 400 Bad Request
            }
            var estudiante = _estudianteRepository.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound(); // 404 Not Found
            }
            var estudianteToPatch = _mapper.Map<EstudianteUpdateDTO>(estudiante);
            estPatch.ApplyTo(estudianteToPatch, ModelState);
            if (!TryValidateModel(estudianteToPatch))
            {
                return ValidationProblem(ModelState); // 422 Unprocessable Entity
            }
            _mapper.Map(estudianteToPatch, estudiante);
            _estudianteRepository.UpdateEstudiante(estudiante);
            _estudianteRepository.GuardarCambios();
            return NoContent(); // 204 No Content
        }
        [HttpDelete("{id}")] // localhost:5000/api/estudiante/{id} [DELETE]
        public ActionResult DeleteEstudiante(int id)
        {
            var estudiante = _estudianteRepository.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound(); // 404 Not Found
            }
            _estudianteRepository.DeleteEstudiante(estudiante);
            _estudianteRepository.GuardarCambios();
            return NoContent(); // 204 No Content
        }
    }
}
