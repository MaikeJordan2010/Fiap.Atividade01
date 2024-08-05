using Atividade01.Dominio.Sistemicas;
using Atividade01.Dominio.ViewModel;

namespace Atividade01.Aplicacao._Contato.Comandos
{
    public interface IContatoComandos
    {
        public Task<ResultadoGenericoComandos> Criar(Contato contato);
        public Task<ResultadoGenericoComandos> Atualizar(Contato contato);
        public Task<ResultadoGenericoComandos> Excluir(string guid);

    }
}
