using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventos.IO.Infra.Data.Mappings
{
    public class EventoMapping : EntityTypeConfiguration<Evento>
    {
        //Esse builder é o mesmo do método onModelCreate
        public override void Map(EntityTypeBuilder<Evento> builder)
        {
            builder
               .ToTable("Eventos");

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder
             .Property(e => e.DescricaoCurta)
             .HasColumnType("varchar(150)");

            builder
             .Property(e => e.DescricaoLonga)
             .HasColumnType("varchar(150)");

            builder
             .Property(e => e.NomeEmpresa)
             .HasColumnType("varchar(150)")
             .IsRequired();

            builder
                .Ignore(e => e.ValidationResult);

            //Pertence ao fluentvalidation
            builder
                .Ignore(e => e.CascadeMode);

            builder
             .Ignore(e => e.Tags);

            //Mapear os relacionamentos
            builder
               .HasOne(e => e.Organizador) //Um evento só pode ter um Organizador
               .WithMany(o => o.Eventos) //Um organizador pode ter varios eventos.
               .HasForeignKey(e => e.OrganizadorId); //Evento vai ter uma FK com Organizador Evento.OrganizadorId = Organizador.organizadorId

            builder
                .HasOne(e => e.Categoria)
                .WithMany(c => c.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);
        }
    }
}
