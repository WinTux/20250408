using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public EstudianteController(IEstudianteRepository estudianteRepository, IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
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
        public ActionResult<EstudianteReadDTO> CreateEstudiante([FromBody] EstudianteCreateDTO estudianteCreateDTO)
        {
            if (estudianteCreateDTO == null)
            {
                return BadRequest(); // 400 Bad Request
            }
            var estudiante = _mapper.Map<Estudiante>(estudianteCreateDTO);
            _estudianteRepository.CreateEstudiante(estudiante);
            _estudianteRepository.GuardarCambios();
            var estudianteReadDTO = _mapper.Map<EstudianteReadDTO>(estudiante);
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
    }
}
