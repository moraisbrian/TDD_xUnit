using CursoOnline.Dados;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CursoOnline.Dominio.Base;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio.Cursos;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped(typeof(ICursoRepositorio), typeof(CursoRepositorio));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ArmazenadorDeCurso>();
        }
    }
}