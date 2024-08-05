using Atividade01.Dominio.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Atividade01.Repositorio.Context
{
    public interface IContatoContext
    {
        public DbSet<Contato> Contato { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
