using Atividade01.Repositorio._ContatoRepositorio.Comandos;
using Atividade01.Repositorio._ContatoRepositorio.Consultas;
using Atividade01.Repositorio.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atividade.CrossCutting
{
    public static class InjecaoDependencias
    {
        public static void AddInfraestrutura(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ContatoContext>(opt => opt.UseInMemoryDatabase("ContatoDB"));

            services.AddScoped< IContatoContext, ContatoContext>();
            services.AddScoped<IContatoComandosRepositorio, ContatoComandosRepositorio>();
            services.AddScoped<IContatoConsultasRepositorio, ContatoConsultasRepositorio>();

        }
    }
}
