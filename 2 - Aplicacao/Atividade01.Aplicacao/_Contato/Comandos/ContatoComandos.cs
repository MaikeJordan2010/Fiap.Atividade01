using Atividade01.Dominio.Sistemicas;
using Atividade01.Dominio.Validadores.Contato;
using Atividade01.Dominio.ViewModel;
using Atividade01.Repositorio._ContatoRepositorio.Comandos;

namespace Atividade01.Aplicacao._Contato.Comandos
{
    public class ContatoComandos : IContatoComandos
    {
        private IContatoComandosRepositorio _contatoRepositorio;
        public ContatoComandos(IContatoComandosRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public async Task<ResultadoGenericoComandos> Criar(Contato contato)
        {
            try
            {

                ValidarCadastrarContato validarCadastrarContato = new();
                var validador = validarCadastrarContato.Validate(contato);

                if (validador.IsValid)
                {
                    await _contatoRepositorio.Inserir(contato);
                    return await Task.FromResult(new ResultadoGenericoComandos(true, "Sucesso ao criar contato!"));
                }

                return await Task.FromResult(new ResultadoGenericoComandos(false, "Erro ao criar contato!", validador.Errors));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ResultadoGenericoComandos> Atualizar(Contato contato)
        {
            try
            {

                ValidarAtualizarContato validarAtualizarContato = new();
                var validador = validarAtualizarContato.Validate(contato);

                if (validador.IsValid)
                {
                    await _contatoRepositorio.Atualizar(contato);
                    return await Task.FromResult(new ResultadoGenericoComandos(true, "Sucesso ao atualizar contato!"));
                }

                return await Task.FromResult(new ResultadoGenericoComandos(false, "Erro ao atualizar contato!", validador.Errors));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        public async Task<ResultadoGenericoComandos> Excluir(string guid)
        {
            try
            {
                if (!string.IsNullOrEmpty(guid))
                {
                    await _contatoRepositorio.Excluir(guid);
                    return await Task.FromResult(new ResultadoGenericoComandos(true, "Sucesso ao excluir contato!"));
                }

                return await Task.FromResult(new ResultadoGenericoComandos(false, "Erro ao excluir contato!"));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}
