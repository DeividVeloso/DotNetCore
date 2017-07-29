﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EFCore.Data;

namespace EFCore.Migrations
{
    [DbContext(typeof(EFCoreContext))]
    [Migration("20170729163919_UpdateEventoDescricao")]
    partial class UpdateEventoDescricao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCore.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EventoId");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("Gratuito");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.ToTable("Eventos");
                });
        }
    }
}
