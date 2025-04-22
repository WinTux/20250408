using Plataforma.DTO;
using System.Text;
using System.Text.Json;

namespace Plataforma.ComunicacionSync.Http
{
    public class ImplHttpCampusHistorialCliente : ICampusHistorialCliente
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ImplHttpCampusHistorialCliente(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task ComunicarseConCampus(EstudianteReadDTO estudiante)
        {
            StringContent cuerpoHttp = new StringContent(JsonSerializer.Serialize(estudiante), Encoding.UTF8, "application/json");
            var respuesta = _httpClient.PostAsync($"{_configuration["CampusService"]}/api/Historial", cuerpoHttp);
            if (respuesta.Result.IsSuccessStatusCode)
                Console.WriteLine("Se ha enviado el estudiante al servicio de Campus (por POST desde Plataforma)");
            else
                Console.WriteLine("NO se ha enviado el estudiante al servicio de Campus (por POST desde Plataforma)");
        }
    }
}
