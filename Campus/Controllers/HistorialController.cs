using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Campus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHistorial()
        {
            // Aquí puedes implementar la lógica para obtener el historial
            // Por ejemplo, podrías llamar a un servicio o acceder a una base de datos
            // Simulación de datos de ejemplo
            var historial = new[]
            {
                new { Id = 1, Fecha = "2023-10-01", Descripcion = "Evento 1" },
                new { Id = 2, Fecha = "2023-10-02", Descripcion = "Evento 2" }
            };
            return Ok(historial);
        }
        [HttpPost]
        public IActionResult AddHistorial()
        {
            Console.WriteLine("Llegó una petición por POST a Historial de Campus");
            return Ok("Respuesta exitosa desde el controlador Historial (Campus)");
        }
    }
}
