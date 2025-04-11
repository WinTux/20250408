using System.ComponentModel.DataAnnotations;

namespace Plataforma.DTO
{
    public class EstudianteCreateDTO
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)] // Ana
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        public string correo { get; set; }
        [Range(60000000, 79999999)]
        public int telefono { get; set; }
    }
}
