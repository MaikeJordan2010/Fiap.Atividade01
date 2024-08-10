using Atividade01.Dominio.ViewModel;
using Atividade01.Dominio.Validadores.Contato;
using FluentAssertions;
using Atividade01.Dominio.Sistemicas;

namespace Atividade01.Testes
{
    public class ConfigurationTests
    {
        [Fact]
        public void Configuration_HttpClientName_DeveRetornarValorCorreto()
        {
            // Act
            var clientName = Configuration.HttpClientName;

            // Assert
            Assert.Equal("APIBrasil", clientName);
        }

        [Fact]
        public void Configuration_UrlAPIBrasil_DeveRetornarValorCorreto()
        {
            // Act
            var url = Configuration.UrlAPIBrasil;

            // Assert
            Assert.Equal("https://brasilapi.com.br", url);
        }
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente_QuandoDadosSaoFornecidos()
        {
            // Arrange
            bool sucesso = true;
            string mensagem = "Operação realizada com sucesso.";
            object dados = new { Id = 1, Nome = "Teste" };

            // Act
            var resultado = new ResultadoGenericoComandos(sucesso, mensagem, dados);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Equal("Operação realizada com sucesso.", resultado.Mensagem);
            Assert.NotNull(resultado.Dados);
            Assert.Equal(1, ((dynamic)resultado.Dados).Id);
            Assert.Equal("Teste", ((dynamic)resultado.Dados).Nome);
        }

        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente_QuandoDadosNaoSaoFornecidos()
        {
            // Arrange
            bool sucesso = false;
            string mensagem = "Ocorreu um erro.";

            // Act
            var resultado = new ResultadoGenericoComandos(sucesso, mensagem);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Ocorreu um erro.", resultado.Mensagem);
            Assert.Null(resultado.Dados);
        }

        [Fact]
        public void Propriedades_DevePermitirAlteracao()
        {
            // Arrange
            var resultado = new ResultadoGenericoComandos(false, "Mensagem inicial", null);

            // Act
            resultado.Sucesso = true;
            resultado.Mensagem = "Mensagem alterada";
            resultado.Dados = new { Id = 2, Nome = "Alterado" };

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Equal("Mensagem alterada", resultado.Mensagem);
            Assert.NotNull(resultado.Dados);
            Assert.Equal(2, ((dynamic)resultado.Dados).Id);
            Assert.Equal("Alterado", ((dynamic)resultado.Dados).Nome);
        }

        public class ValidarCadastrarContatoTests
        {
            private readonly ValidarCadastrarContato _validador;

            public ValidarCadastrarContatoTests()
            {
                _validador = new ValidarCadastrarContato();
            }

            [Fact]
            public void Validador_DeveValidarCorretamente_QuandoDadosSaoValidos()
            {
                // Arrange
                var contato = new Contato
                {
                    Nome = "João da Silva",
                    Estado = "SP",
                    Municipio = "São Paulo",
                    DDD = "11",
                    Telefone = "987654321",
                    Email = "joao.silva@example.com"
                };

                // Act
                var resultado = _validador.Validate(contato);

                // Assert
                resultado.IsValid.Should().BeTrue();
                resultado.Errors.Should().BeEmpty();
            }

            [Fact]
            public void Validador_DeveRetornarErro_QuandoNomeENuloOuVazio()
            {
                // Arrange
                var contato = new Contato
                {
                    Nome = null, // Nome nulo
                    Estado = "SP",
                    Municipio = "São Paulo",
                    DDD = "11",
                    Telefone = "987654321",
                    Email = "joao.silva@example.com"
                };

                // Act
                var resultado = _validador.Validate(contato);

                // Assert
                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O Nome não pode ser nulo");

                // Testando o Nome vazio
                contato.Nome = "";
                resultado = _validador.Validate(contato);

                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O Nome não pode ser vazio!");
            }

            [Fact]
            public void Validador_DeveRetornarErro_QuandoEstadoOuMunicipioENuloOuVazio()
            {
                // Arrange
                var contato = new Contato
                {
                    Nome = "João da Silva",
                    Estado = null, // Estado nulo
                    Municipio = null, // Município nulo
                    DDD = "11",
                    Telefone = "987654321",
                    Email = "joao.silva@example.com"
                };

                // Act
                var resultado = _validador.Validate(contato);

                // Assert
                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Estado" && e.ErrorMessage == "O Estado não pode ser nulo");
                resultado.Errors.Should().Contain(e => e.PropertyName == "Municipio" && e.ErrorMessage == "O Municipio não pode ser nulo");

                // Testando Estado e Município vazios
                contato.Estado = "";
                contato.Municipio = "";
                resultado = _validador.Validate(contato);

                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Estado" && e.ErrorMessage == "O Estado não pode ser vazio!");
                resultado.Errors.Should().Contain(e => e.PropertyName == "Municipio" && e.ErrorMessage == "O Municipio não pode ser vazio!");
            }

            [Fact]
            public void Validador_DeveRetornarErro_QuandoDDDEInvalido()
            {
                // Arrange
                var contato = new Contato
                {
                    Nome = "João da Silva",
                    Estado = "SP",
                    Municipio = "São Paulo",
                    DDD = "123", // DDD inválido
                    Telefone = "987654321",
                    Email = "joao.silva@example.com"
                };

                // Act
                var resultado = _validador.Validate(contato);

                // Assert
                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "DDD" && e.ErrorMessage == "O DDD não pode ter mais de 2 digitos");

                // Testando DDD com menos de 2 dígitos
                contato.DDD = "1";
                resultado = _validador.Validate(contato);

                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "DDD" && e.ErrorMessage == "O DDD não pode ter menos de 2 digitos");
            }

            [Fact]
            public void Validador_DeveRetornarErro_QuandoTelefoneEInvalido()
            {
                // Arrange
                var contato = new Contato
                {
                    Nome = "João da Silva",
                    Estado = "SP",
                    Municipio = "São Paulo",
                    DDD = "11",
                    Telefone = "1234567890", // Telefone inválido
                    Email = "joao.silva@example.com"
                };

                // Act
                var resultado = _validador.Validate(contato);

                // Assert
                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Telefone" && e.ErrorMessage == "O telefone não pode ter mais de 9 digitos");

                // Testando telefone com menos de 8 dígitos
                contato.Telefone = "1234567";
                resultado = _validador.Validate(contato);

                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Telefone" && e.ErrorMessage == "O telefone não pode ter menos de 8 digitos");
            }

            [Fact]
            public void Validador_DeveRetornarErro_QuandoEmailEInvalido()
            {
                // Arrange
                var contato = new Contato
                {
                    Nome = "João da Silva",
                    Estado = "SP",
                    Municipio = "São Paulo",
                    DDD = "11",
                    Telefone = "987654321",
                    Email = "emailinvalido.com" // Email inválido
                };

                // Act
                var resultado = _validador.Validate(contato);

                // Assert
                resultado.IsValid.Should().BeFalse();
                resultado.Errors.Should().Contain(e => e.PropertyName == "Email" && e.ErrorMessage == "O e-mail está com formato incorreto");
            }
        }
    }
}