using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Models;
using Plataforma.Repositories;

namespace Plataforma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository _estudianteRepository;
        public EstudianteController(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Estudiante>> GetEstudiantes()
        {
            var estudiantes = _estudianteRepository.GetEstudiantes();
            return Ok(estudiantes); // 200 OK
        }
        [HttpGet("{id}")]
        public ActionResult<Estudiante> GetEstudianteById(int id)
        {
            var estudiante = _estudianteRepository.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(estudiante); // 200 OK
        }
    }
}
