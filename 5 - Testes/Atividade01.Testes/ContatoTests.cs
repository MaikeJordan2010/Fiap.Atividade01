using Atividade01.Dominio.ViewModel;
using Atividade01.Repositorio._ContatoRepositorio.Comandos;
using Atividade01.Repositorio._ContatoRepositorio.Consultas;
using Atividade01.Repositorio.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Atividade01.Testes
{
    public class ContatoTests
    {
        [Fact]
        public void Contato_ViewModel()
        {
            // Arrange
            string guid = "12345";
            string nome = "João";
            string email = "joao@example.com";
            string ddd = "11";
            string telefone = "987654321";
            string estado = "SP";
            string municipio = "São Paulo";


            // Act 
            var contato = new Contato(nome, email, ddd, telefone, municipio, estado, guid);

            // Assert
            Assert.Equal(guid, contato.Guid);
            Assert.Equal(nome, contato.Nome);
            Assert.Equal(email, contato.Email);
            Assert.Equal(ddd, contato.DDD);
            Assert.Equal(telefone, contato.Telefone);
            Assert.Equal(estado, contato.Estado);
            Assert.Equal(municipio, contato.Municipio);
        }

        [Fact]
        public void ContatoConsultasRepositorio_ObterLista()
        {
            // Arrange
            var mock = new Mock<IContatoContext>();
            var contatoConsultasRepositorio = new ContatoConsultasRepositorio(mock.Object);

            // Act
            var resultado = contatoConsultasRepositorio.ObterLista();

            // Assert
            Assert.NotNull(resultado);
        }

        [Fact]
        public void ContatoConsultasRepositorio_ObterListaPorDDD()
        {
            // Arrange
            var mock = new Mock<IContatoContext>();
            var contatoConsultasRepositorio = new ContatoConsultasRepositorio(mock.Object);

            // Act
            var resultado = contatoConsultasRepositorio.ObterListaPorDDD("11");

            // Assert
            Assert.NotNull(resultado);
        }

        public class ContatoContextTests
        {
            private DbContextOptions<ContatoContext> CreateInMemoryOptions(string dbName)
            {
                return new DbContextOptionsBuilder<ContatoContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;
            }

            [Fact]
            public void ContatoContext_DeveCriarInstanciaCorretamente()
            {
                // Arrange
                var options = CreateInMemoryOptions(nameof(ContatoContext_DeveCriarInstanciaCorretamente));

                // Act
                using var context = new ContatoContext(options);

                // Assert
                context.Should().NotBeNull();
            }

            [Fact]
            public void ContatoContext_DeveAdicionarContato()
            {
                // Arrange
                var options = CreateInMemoryOptions(nameof(ContatoContext_DeveAdicionarContato));

                using var context = new ContatoContext(options);
                var contato = new Contato
                {
                    Guid = Guid.NewGuid().ToString(),
                    Nome = "Maria Silva",
                    Email = "maria.silva@example.com",
                    DDD = "11",
                    Telefone = "987654321",
                    Estado = "SP",
                    Municipio = "São Paulo"
                };

                // Act
                context.Contato.Add(contato);
                context.SaveChanges();

                // Assert
                context.Contato.Should().ContainSingle();
                var storedContato = context.Contato.First();
                storedContato.Nome.Should().Be("Maria Silva");
                storedContato.Email.Should().Be("maria.silva@example.com");
            }

            [Fact]
            public void ContatoContext_DeveBuscarContatoPorId()
            {
                // Arrange
                var options = CreateInMemoryOptions(nameof(ContatoContext_DeveBuscarContatoPorId));

                using var context = new ContatoContext(options);
                var contato = new Contato
                {
                    Guid = Guid.NewGuid().ToString(),
                    Nome = "João Pereira",
                    Email = "joao.pereira@example.com",
                    DDD = "21",
                    Telefone = "987654321",
                    Estado = "RJ",
                    Municipio = "Rio de Janeiro"
                };

                context.Contato.Add(contato);
                context.SaveChanges();

                // Act
                var fetchedContato = context.Contato.Find(contato.Guid);

                // Assert
                fetchedContato.Should().NotBeNull();
                fetchedContato.Nome.Should().Be("João Pereira");
                fetchedContato.Email.Should().Be("joao.pereira@example.com");
            }

            [Fact]
            public void ContatoContext_DeveAtualizarContato()
            {
                // Arrange
                var options = CreateInMemoryOptions(nameof(ContatoContext_DeveAtualizarContato));

                using var context = new ContatoContext(options);
                var contato = new Contato
                {
                    Guid = Guid.NewGuid().ToString(),
                    Nome = "Ana Souza",
                    Email = "ana.souza@example.com",
                    DDD = "31",
                    Telefone = "987654321",
                    Estado = "MG",
                    Municipio = "Belo Horizonte"
                };

                context.Contato.Add(contato);
                context.SaveChanges();

                // Act
                contato.Nome = "Ana Souza Atualizado";
                contato.Email = "ana.souza.atualizado@example.com";
                context.Contato.Update(contato);
                context.SaveChanges();

                // Assert
                var updatedContato = context.Contato.Find(contato.Guid);
                updatedContato.Nome.Should().Be("Ana Souza Atualizado");
                updatedContato.Email.Should().Be("ana.souza.atualizado@example.com");
            }

            [Fact]
            public void ContatoContext_DeveDeletarContato()
            {
                // Arrange
                var options = CreateInMemoryOptions(nameof(ContatoContext_DeveDeletarContato));

                using var context = new ContatoContext(options);
                var contato = new Contato
                {
                    Guid = Guid.NewGuid().ToString(),
                    Nome = "Carlos Lima",
                    Email = "carlos.lima@example.com",
                    DDD = "71",
                    Telefone = "987654321",
                    Estado = "BA",
                    Municipio = "Salvador"
                };

                context.Contato.Add(contato);
                context.SaveChanges();

                // Act
                context.Contato.Remove(contato);
                context.SaveChanges();

                // Assert
                context.Contato.Should().BeEmpty();
            }
        }
        public class ContatoComandosRepositorioTests
        {
            private DbContextOptions<ContatoContext> CreateInMemoryOptions(string dbName)
            {
                return new DbContextOptionsBuilder<ContatoContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;
            }

            private ContatoComandosRepositorio CreateRepositorioWithContext(string dbName)
            {
                var options = CreateInMemoryOptions(dbName);
                var context = new ContatoContext(options);
                var mockContext = new Mock<IContatoContext>();
                mockContext.Setup(c => c.Contato).Returns(context.Contato);
                mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

                return new ContatoComandosRepositorio(mockContext.Object);
            }


            [Fact]
            public async Task Excluir_DeveRemoverContato()
            {
                // Arrange
                var repositorio = CreateRepositorioWithContext(nameof(Excluir_DeveRemoverContato));
                var contato = new Contato
                {
                    Guid = Guid.NewGuid().ToString(),
                    Nome = "Pedro Lima",
                    Email = "pedro.lima@example.com",
                    DDD = "31",
                    Telefone = "987654321",
                    Estado = "MG",
                    Municipio = "Belo Horizonte"
                };

                await repositorio.Inserir(contato);

                // Act
                await repositorio.Excluir(contato.Guid);

                // Assert
                var context = repositorio.GetType()
                    .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(repositorio) as IContatoContext;

                var deletedContato = await context.Contato.FirstOrDefaultAsync(c => c.Guid == contato.Guid);
                deletedContato.Should().BeNull();
            }
        }

    }
}
