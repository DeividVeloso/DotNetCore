using Eventos.IO.Domain.Eventos;
using System;

namespace Eventos.IO.Domain.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventos = new Evento
                (
                "",
                DateTime.Now,
                DateTime.Now,
                true,
                50,
                false,
                "Veloso S.A."
                );
            Console.WriteLine(eventos.ToString());
            Console.WriteLine(eventos.EhValido());

            if (!eventos.ValidationResult.IsValid)
            {
                foreach (var erro in eventos.ValidationResult.Errors)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
            }

            Console.ReadKey();
            
        }
    }
}