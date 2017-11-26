using Eventos.IO.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.Commands
{
    public class IncluirEnderecoCommand : Command
    {
        public IncluirEnderecoCommand()
        {

        }

        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        //Esse Endereço pertence a QUEM?
        public Guid? EventoId { get; set; }
    }
}
