using Atividade01.Dominio.ViewModel;

namespace Atividade01.Repositorio._ContatoRepositorio.Comandos
{
    public interface IContatoComandosRepositorio
    {
        public Task Inserir(Contato contato);
        public Task Atualizar(Contato contato);
        public Task Excluir(string guid);
      

    }
}
