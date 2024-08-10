using Atividade01.Dominio.ViewModel;
using Atividade01.Dominio.Validadores.Contato;
using FluentAssertions;

namespace Atividade01.Testes
{
    public class ValidarAtualizarContatoTests
    {
        private readonly ValidarAtualizarContato _validador;

        public ValidarAtualizarContatoTests()
        {
            _validador = new ValidarAtualizarContato();
        }

        [Fact]
        public void Validador_DeveValidarCorretamente_QuandoDadosSaoValidos()
        {
            // Arrange
            var contato = new Contato
            {
                Guid = "123456",
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
        public void Validador_DeveRetornarErro_QuandoGuidENuloOuVazio()
        {
            // Arrange
            var contato = new Contato
            {
                Guid = null,
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
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.PropertyName == "Guid" && e.ErrorMessage == "O Guid não pode ser nulo");

            // Testando o Guid vazio
            contato.Guid = "";
            resultado = _validador.Validate(contato);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.PropertyName == "Guid" && e.ErrorMessage == "O Guid não pode ser vazio!");
        }

        [Fact]
        public void Validador_DeveRetornarErro_QuandoNomeENuloOuVazio()
        {
            // Arrange
            var contato = new Contato
            {
                Guid = "123456",
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
                Guid = "123456",
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
                Guid = "123456",
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
                Guid = "123456",
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
                Guid = "123456",
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
