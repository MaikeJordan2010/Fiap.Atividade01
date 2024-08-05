using Atividade01.Dominio.ViewModel;

namespace Atividade01.Aplicacao._Contato.Consultas
{
    public interface IContatoConsultas
    {
        public Task<IEnumerable<Contato>> ObterLista();
        public Task<Contato?> ObterPorId(string guid);
        public Task<IEnumerable<Contato>> ObterListaPorDDD(string ddd);
    }
}
