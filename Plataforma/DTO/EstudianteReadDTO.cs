using System.ComponentModel.DataAnnotations;

namespace Plataforma.DTO
{
    public class EstudianteReadDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string? apellido { get; set; } // Admite nulos
        public int telefono { get; set; }
    }
}
