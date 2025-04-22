using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Plataforma.ComunicacionSync.Http;
using Plataforma.Repositories;

namespace Plataforma
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(s=>s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddHttpClient<ICampusHistorialCliente, ImplHttpCampusHistorialCliente>();
            builder.Services.AddDbContext<InstitutoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("una_conexion")));
            // DbContext (agregar)
            builder.Services.AddScoped<IEstudianteRepository, ImplEstudianteRepository>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
