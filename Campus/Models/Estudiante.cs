using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Estudiante
    {
        [Key]
        [Required]
        public int ci { get; set; }
        [Required]
        public int fci { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)] // Ana
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        public string correo { get; set; }
        [Range(60000000, 79999999)]
        public int telefono { get; set; }
        public ICollection<Perfil> perfiles { get; set; } = new List<Perfil>();
    }
}
