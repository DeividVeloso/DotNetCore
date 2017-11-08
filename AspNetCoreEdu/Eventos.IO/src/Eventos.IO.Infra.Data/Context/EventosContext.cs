using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Organizadores;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Evento>()
                .Ignore(e => e.Tags);

            //Pertence ao fluentvalidation
            modelBuilder.Entity<Evento>()
                .Ignore(e => e.CascadeMode);

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

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }


}
