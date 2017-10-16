using Eventos.IO.Domain.Core.Models;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos
{
    public class Categoria : Entity<Categoria>
    {
        public Categoria(string nome)
        {
            Nome = nome;
        }
        protected Categoria()
        {

        }
        //Fazer relacionamento onde uma Categoria pode ter Variaos Eventos
        public ICollection<Evento> Eventos { get; set; }

        public string Nome { get; private set; }
        public override bool EhValido()
        {
            return true;
        }
    }
}