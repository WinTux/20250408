using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plataforma.DTO;
using Plataforma.Models;
using Plataforma.Repositories;

namespace Plataforma.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            var estudiantes = _estudianteRepository.GetEstudiantes();
            return Ok(_mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes)); // 200 OK
        }
        [HttpGet("{id}")]
        public ActionResult<EstudianteReadDTO> GetEstudianteById(int id)
        {
            var estudiante = _estudianteRepository.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(_mapper.Map<EstudianteReadDTO>(estudiante)); // 200 OK
        }
    }
}
