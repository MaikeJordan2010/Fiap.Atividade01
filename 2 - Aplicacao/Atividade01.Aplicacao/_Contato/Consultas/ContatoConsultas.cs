using Atividade01.Dominio.ViewModel;
using Atividade01.Repositorio._ContatoRepositorio.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade01.Aplicacao._Contato.Consultas
{
    public class ContatoConsultas : IContatoConsultas
    {
        private readonly IContatoConsultasRepositorio _contatoRepositorio;

        public ContatoConsultas(IContatoConsultasRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public Task<IEnumerable<Contato>> ObterLista()
        {
            try
            {
                return _contatoRepositorio.ObterLista();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<IEnumerable<Contato>> ObterListaPorDDD(string ddd)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddd))
                {
                    return await _contatoRepositorio.ObterListaPorDDD(ddd);
                }

                return await ObterLista();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Contato?> ObterPorId(string guid)
        {
            try
            {
                if (!string.IsNullOrEmpty(guid))
                {
                    return await _contatoRepositorio.ObterPorId(guid);
                }

                return default;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
