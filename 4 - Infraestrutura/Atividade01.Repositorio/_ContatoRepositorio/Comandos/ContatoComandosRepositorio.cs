using Atividade01.Dominio.ViewModel;
using Atividade01.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace Atividade01.Repositorio._ContatoRepositorio.Comandos
{
    public class ContatoComandosRepositorio : IContatoComandosRepositorio
    {
        private readonly IContatoContext _context;

        public ContatoComandosRepositorio(IContatoContext context)
        {
            _context = context;
        }

        public async Task Inserir(Contato contato)
        {
            _context.Contato.Add(contato);
            await Task.Run(() => _context.SaveChangesAsync());
        }

        public async Task Atualizar(Contato contato)
        {
            _context.Contato.Update(contato);
            await Task.Run(() => _context.SaveChangesAsync());
        }

        public async Task Excluir(string guid)
        {
            var contato = await _context.Contato.FirstOrDefaultAsync(x => x.Guid == guid);

            if (contato != null)
            { 
                _context.Contato.Remove(contato!);
                await Task.Run(() => _context.SaveChangesAsync());
            }
        }


        

    }
}
