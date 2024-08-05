using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade01.Dominio.Sistemicas
{
    public class ResultadoGenericoComandos
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public object? Dados { get; set; }

        public ResultadoGenericoComandos(bool sucesso, string mensagem, object? dados = null)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Dados = dados;
        }
    }
}
