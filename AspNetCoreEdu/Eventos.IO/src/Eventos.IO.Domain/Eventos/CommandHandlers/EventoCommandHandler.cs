using Eventos.IO.Domain.CommandHandlers;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Events;
using Eventos.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.CommandHandlers
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>,
        IHandler<AtualizarEventoCommand>,
        IHandler<ExcluirEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IBus _bus;

        public EventoCommandHandler(IEventoRepository eventoRepository, IUnitOfWork uow, IBus bus) : base(uow, bus)
        {
            _eventoRepository = eventoRepository;
            _bus = bus;
        }

        public void Handle(RegistrarEventoCommand message)
        {
            var evento = new Evento(
                message.Nome,
                message.DataIncio,
                message.DataFim,
                message.Gratuito,
                message.Valor,
                message.Online,
                message.NomeEmpresa
                );

            if (!evento.EhValido())
            {
                Notificacoes(evento.ValidationResult);
                return;
            }

            //Posso colocar mais validações
            if (Commit())
            {
                //Notificar processo concluido!
                Console.WriteLine("Evento registrado com sucesso!");
                //Para fazer isso vamos usar um BUS
                _bus.RaiseEvent(new EventoRegistradoEvent(
                                    evento.Id,
                                    evento.Nome,
                                    evento.DataIncio,
                                    evento.DataFim,
                                    evento.Gratuito,
                                    evento.Valor,
                                    evento.Online,
                                    evento.NomeEmpresa)
                             );
            }

            //Aqui salvo no meu banco de dados as informações de entrada.
            _eventoRepository.Add(evento);
        }

        public void Handle(AtualizarEventoCommand message)
        {
            throw new NotImplementedException();
        }

        public void Handle(ExcluirEventoCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
