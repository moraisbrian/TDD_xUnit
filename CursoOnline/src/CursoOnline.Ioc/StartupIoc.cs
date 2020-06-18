using CursoOnline.Dados;
using Microsoft.Extensions.DependencyInjection;
using CursoOnline.Dominio.Base;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio.Cursos;
using Microsoft.EntityFrameworkCore;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;

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
            services.AddScoped(typeof(IAlunoRepositorio), typeof(AlunoRepositorio));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IConversorDePublicoAlvo), typeof(ConversorDePublicoAlvo));
            services.AddScoped<ArmazenadorDeCurso>();
            services.AddScoped<ArmazenadorDeAluno>();
        }
    }
}