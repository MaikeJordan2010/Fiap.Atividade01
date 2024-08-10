using Atividade01.Dominio.ViewModel;
using FluentAssertions;

namespace Atividade01.Testes
{
    public class MunicipiosTests
    {
        [Fact]
        public void Municipios_DeveTerPropriedadesInicializadasCorretamente()
        {
            // Arrange & Act
            var municipios = new Municipios();

            // Assert
            municipios.State.Should().BeNull();
            municipios.Cities.Should().NotBeNull();
            municipios.Cities.Should().BeEmpty();
        }

        [Fact]
        public void Municipios_DevePermitirAtribuicaoDeEstado()
        {
            // Arrange
            var municipios = new Municipios();
            var estado = "SP";

            // Act
            municipios.State = estado;

            // Assert
            municipios.State.Should().Be(estado);
        }

        [Fact]
        public void Municipios_DevePermitirAtribuicaoDeCidades()
        {
            // Arrange
            var municipios = new Municipios();
            var cidades = new string[] { "São Paulo", "Campinas", "Santos" };

            // Act
            municipios.Cities = cidades;

            // Assert
            municipios.Cities.Should().NotBeNull();
            municipios.Cities.Should().HaveCount(3);
            municipios.Cities.Should().ContainInOrder("São Paulo", "Campinas", "Santos");
        }

        [Fact]
        public void Municipios_DevePermitirCidadesNulo()
        {
            // Arrange
            var municipios = new Municipios();

            // Act
            municipios.Cities = null;

            // Assert
            municipios.Cities.Should().BeNull();
        }
    }
}
