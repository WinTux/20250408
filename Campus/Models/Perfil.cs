using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Perfil
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string nick { get; set; }
        [Required]
        public string descripcion { get; set; }
        public string lenguajes { get; set; }
        public int estudianteCI { get; set; }
        public Estudiante estudiante { get; set; }
    }
}
