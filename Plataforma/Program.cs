using Microsoft.EntityFrameworkCore;
using Plataforma.Repositories;

namespace Plataforma
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
