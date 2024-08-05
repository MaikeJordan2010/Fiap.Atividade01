using Atividade01.Dominio.ViewModel;

namespace Atividade01.Repositorio._ContatoRepositorio.Consultas
{
    public interface IContatoConsultasRepositorio
    {
        public Task<IEnumerable<Contato>> ObterLista();
        public Task<IEnumerable<Contato>> ObterListaPorDDD(string ddd);
        public Task<Contato?> ObterPorId(string guid);
    }
}
