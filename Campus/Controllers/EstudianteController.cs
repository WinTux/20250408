using AutoMapper;
using Campus.DTO;
using Campus.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/p/[controller]")] // https://localhost:5000/api/p/Estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IPerfilRepository perfilRepository;
        private readonly IMapper mapper;
        public EstudianteController(IPerfilRepository perfilRepository, IMapper mapper)
        {
            this.perfilRepository = perfilRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            Console.WriteLine("Se obtienen estudiantes (mediante servicio Campus)");
            var estudiantes = perfilRepository.GetEstudiantes();
            var estudiantesReadDTO = mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes);
            return Ok(estudiantesReadDTO);
        }
    }
}
