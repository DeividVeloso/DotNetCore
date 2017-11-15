using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventos.IO.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public override void Map(EntityTypeBuilder<Endereco> builder)
        {

            builder.Property(e => e.Logradouro)
            .HasColumnType("varchar(150)")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(e => e.Numero)
                .HasMaxLength(20)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(e => e.Bairro)
              .HasMaxLength(50)
              .HasColumnType("varchar(50)")
              .IsRequired();

            builder.Property(e => e.CEP)
              .HasMaxLength(8)
              .HasColumnType("varchar(8)")
              .IsRequired();

            builder.Property(e => e.Complemento)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

            builder.Property(e => e.Cidade)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

            builder.Property(e => e.Estado)
             .HasMaxLength(100)
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder
               .HasOne(c => c.Evento) //Um Endereço só tem um Evento
               .WithOne(c => c.Endereco) //Um Evento só tem um Endereço
               .HasForeignKey<Endereco>(c => c.EventoId)
               .IsRequired(false);

            builder
             .Ignore(e => e.ValidationResult);

            //Pertence ao fluentvalidation
            builder
                  .Ignore(e => e.CascadeMode);

            builder.ToTable("Enderecos");
        }
    }
}
