using System;

namespace Eventos.IO.Domain.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventos = new Models.Evento
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

            if (!eventos.EhValido())
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