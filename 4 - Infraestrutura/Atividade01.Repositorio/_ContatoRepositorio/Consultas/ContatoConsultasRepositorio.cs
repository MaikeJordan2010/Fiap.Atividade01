using Atividade01.Dominio.ViewModel;
using Atividade01.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace Atividade01.Repositorio._ContatoRepositorio.Consultas
{
    public class ContatoConsultasRepositorio : IContatoConsultasRepositorio
    {
        private readonly IContatoContext _context;
        public ContatoConsultasRepositorio(IContatoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> ObterLista()
        {
            var resultado = await _context.Contato.ToArrayAsync();

            return resultado ?? Enumerable.Empty<Contato>();
        }

        public async Task<IEnumerable<Contato>> ObterListaPorDDD(string ddd)
        {
            var resultado = await _context.Contato.Where(x => x.DDD!.Contains(ddd) || x.DDD == ddd).ToArrayAsync();

            return resultado ?? Enumerable.Empty<Contato>();
        }


        public async Task<Contato?> ObterPorId(string guid)
        {
            var resultado = await _context.Contato.FirstOrDefaultAsync(x => x.Guid == guid);

            return resultado ?? default;
        }
    }
}
