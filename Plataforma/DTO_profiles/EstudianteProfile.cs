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
            CreateMap<Estudiante, EstudianteUpdateDTO>();
                /*.ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.nombre))
                .ForMember(dest => dest.apellido, opt => opt.MapFrom(src => src.apellido))
                .ForMember(dest => dest.telefono, opt => opt.MapFrom(src => src.telefono));*/
        }
    }
}
