using Atividade01.Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Atividade01.Aplicacao.Services
{
    public class ObterMunicipios : IObterMunicipios
    {
        private readonly IHttpClientFactory _httpClient;

        public ObterMunicipios(IHttpClientFactory httpClient)
        {
                _httpClient = httpClient;
        }
        public async Task<Municipios?> Get(string ddd)
        {
            try
            {
                var client = _httpClient.CreateClient();

                var result = await client.GetAsync($"https://brasilapi.com.br/api/ddd/v1/{ddd}");

                if(result.IsSuccessStatusCode)
                {

                    var resultado = await result.Content.ReadAsStringAsync();

                    return await Task.FromResult(JsonSerializer.Deserialize<Municipios>(resultado));
                }

                return null;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
