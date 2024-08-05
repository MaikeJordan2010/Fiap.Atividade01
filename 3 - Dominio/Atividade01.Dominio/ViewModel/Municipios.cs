using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Atividade01.Dominio.ViewModel
{
    public class Municipios
    {
        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("cities")]
        public string[]? Cities { get; set; } = [];
    }
}
