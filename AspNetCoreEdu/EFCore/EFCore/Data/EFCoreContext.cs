using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public class EFCoreContext : DbContext
    {
        //Ele vai receber essas opções via injeção de dependência
        //E passar para nossa classe base DbContext
        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {

        }

        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Construindo minha tabela, usando minha Model
            modelBuilder.Entity<Evento>()
                .Property(x => x.Id)
                .HasColumnName("EventoId");

            modelBuilder.Entity<Evento>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Evento>()
                .Property(x => x.Descricao)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);
                

            base.OnModelCreating(modelBuilder);
        }
    }
}
