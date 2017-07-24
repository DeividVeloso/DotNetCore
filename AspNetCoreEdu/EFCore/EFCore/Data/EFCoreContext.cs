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
    }
}
