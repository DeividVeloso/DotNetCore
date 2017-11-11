using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Organizadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Eventos.IO.Infra.Data.Context
{
    public class EventosContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FlentAPI

            #region Evento
            modelBuilder.Entity<Evento>()
                .ToTable("Eventos");

            modelBuilder.Entity<Evento>()
                .Property(e => e.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            modelBuilder.Entity<Evento>()
             .Property(e => e.DescricaoCurta)
             .HasColumnType("varchar(150)");

            modelBuilder.Entity<Evento>()
             .Property(e => e.DescricaoLonga)
             .HasColumnType("varchar(150)");

            modelBuilder.Entity<Evento>()
             .Property(e => e.NomeEmpresa)
             .HasColumnType("varchar(150)")
             .IsRequired();

            modelBuilder.Entity<Evento>()
                .Ignore(e => e.ValidationResult);

            //Pertence ao fluentvalidation
            modelBuilder.Entity<Evento>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Evento>()
             .Ignore(e => e.Tags);

            //Mapear os relacionamentos
            modelBuilder.Entity<Evento>()
               .HasOne(e => e.Organizador) //Um evento só pode ter um Organizador
               .WithMany(o => o.Eventos) //Um organizador pode ter varios eventos.
               .HasForeignKey(e => e.OrganizadorId); //Evento vai ter uma FK com Organizador Evento.OrganizadorId = Organizador.organizadorId

            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Categoria)
                .WithMany(c => c.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);

            #endregion

            #region Endereço
            modelBuilder.Entity<Endereco>()
                .HasOne(c => c.Evento) //Um Endereço só tem um Evento
                .WithOne(c => c.Endereco) //Um Evento só tem um Endereço
                .HasForeignKey<Endereco>(c => c.EventoId)
                .IsRequired(false);

            modelBuilder.Entity<Endereco>()
             .Ignore(e => e.ValidationResult);

            //Pertence ao fluentvalidation
            modelBuilder.Entity<Endereco>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            #endregion

            #region Organizador

            modelBuilder.Entity<Organizador>()
             .Ignore(e => e.ValidationResult);

            //Pertence ao fluentvalidation
            modelBuilder.Entity<Organizador>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Organizador>().ToTable("Organizadores");
            #endregion

            #region Categoria
            modelBuilder.Entity<Categoria>()
           .Ignore(e => e.ValidationResult);

            //Pertence ao fluentvalidation
            modelBuilder.Entity<Categoria>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder() //Monta uma configuração
                .SetBasePath(Directory.GetCurrentDirectory()) //é a raiz no meu projeto
                .AddJsonFile("appsettings.json") //Onde vai estar minha connection string
                .Build();
            //Usa o SQL SERVER e pega a minha connection string
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }


}
