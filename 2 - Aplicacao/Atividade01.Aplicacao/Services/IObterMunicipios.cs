using Atividade01.Dominio.ViewModel;

namespace Atividade01.Aplicacao.Services
{
    public interface IObterMunicipios
    {
        public Task<Municipios?> Get(string ddd);
    }
}
