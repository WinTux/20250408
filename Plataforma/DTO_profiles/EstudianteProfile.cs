using AutoMapper;
using Plataforma.DTO;
using Plataforma.Models;

namespace Plataforma.DTO_profiles
{
    public class EstudianteProfile : Profile
    {
        public EstudianteProfile()
        {
            CreateMap<Estudiante, EstudianteReadDTO>(); // ----->
            CreateMap<EstudianteCreateDTO, Estudiante>(); // ----->
            CreateMap<EstudianteUpdateDTO, Estudiante>();
        }
    }
}
