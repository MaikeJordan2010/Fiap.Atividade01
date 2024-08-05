using Atividade01.Dominio.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Atividade01.Repositorio.Context
{
    public class ContatoContext : DbContext, IContatoContext
    {
        public DbSet<Contato> Contato { get; set; }

        public ContatoContext(DbContextOptions<ContatoContext> options) : base(options)
        {
                
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Contato>().HasData(
        //            new Contato("Maike","maikejordan@gmail.com", "11","963861602","SP","Guarulhos", 1000),
        //            new Contato("Rodrigo", "Rodrigo@gmail.com", "11", "963861606", "SP", "São Paulo",1001)
        //    );

        //    .(modelBuilder);
        //}

    }
}
