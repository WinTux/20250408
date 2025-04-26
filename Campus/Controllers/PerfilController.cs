using AutoMapper;
using Campus.DTO;
using Campus.Models;
using Campus.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/[controller]/estudiante/{estudianteci}")] // https://localhost:5000/api/Perfil/estudiante/12345678
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository perfilRepository;
        private readonly IMapper mapper;
        public PerfilController(IPerfilRepository perfilRepository, IMapper mapper)
        {
            this.perfilRepository = perfilRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PerfilReadDTO>> GetPerfilesDeEstudiante(int estudianteci)
        {
            Console.WriteLine("Se obtienen perfiles de estudiante (mediante servicio Campus)");
            if (!perfilRepository.ExisteEstudiante(estudianteci))
            {
                return NotFound();
            }
            var perfiles = perfilRepository.GetPerfilesDeEstudiante(estudianteci);
            var perfilesReadDTO = mapper.Map<IEnumerable<PerfilReadDTO>>(perfiles);
            return Ok(perfilesReadDTO);
        }
        [HttpGet("{idPerfil}", Name = "GetPerfil")] // https://localhost:5000/api/Perfil/estudiante/12345678/1
        public ActionResult<PerfilReadDTO> GetPerfil(int idPerfil, int estudianteci)
        {
            Console.WriteLine("Se obtiene perfil (mediante servicio Campus)");
            if (!perfilRepository.ExisteEstudiante(estudianteci))
            {
                return NotFound();
            }
            var perfil = perfilRepository.GetPerfil(idPerfil, estudianteci);
            if (perfil == null)
            {
                return NotFound();
            }
            var perfilReadDTO = mapper.Map<PerfilReadDTO>(perfil);
            return Ok(perfilReadDTO);
        }
        [HttpPost] // https://localhost:5000/api/Perfil/estudiante/12345678 [POST]
        public ActionResult<PerfilReadDTO> CrearPerfil(int estudianteci, PerfilCreateDTO perfilCreateDTO)
        {
            Console.WriteLine("Se crea perfil (mediante servicio Campus)");
            if (!perfilRepository.ExisteEstudiante(estudianteci))
            {
                return NotFound();
            }
            var perfil = mapper.Map<Perfil>(perfilCreateDTO);
            perfilRepository.CrearPerfil(estudianteci, perfil);
            perfilRepository.Guardar();
            var perfilReadDTO = mapper.Map<PerfilReadDTO>(perfil);
            return CreatedAtRoute(nameof(GetPerfil), new { estudianteci = estudianteci, idPerfil = perfil.id }, perfilReadDTO);
        }
    }
}
