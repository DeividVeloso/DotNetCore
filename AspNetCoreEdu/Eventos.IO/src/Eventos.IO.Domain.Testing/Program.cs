﻿using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Eventos.Commands;
using System;
using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Eventos.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Domain.Eventos.Events;

namespace Eventos.IO.Domain.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new FakeBus();
            //Registro com sucesso
            var cmd = new RegistrarEventoCommand(
                "JsExperience",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                true,
                0,
                true,
                "IMaster"
                );

            Inicio(cmd);
            bus.SendCommand(cmd);
            Fim(cmd);

            //Registro com falha
            cmd = new RegistrarEventoCommand(
               "",
               DateTime.Now.AddDays(2),
               DateTime.Now.AddDays(1),
               false,
               0,
               false,
               ""
               );

            Inicio(cmd);
            bus.SendCommand(cmd);
            Fim(cmd);

            //Atualizar Evento
            var cmd2 = new AtualizarEventoCommand(
                Guid.NewGuid(),
                "JsExperience",
                "",
                "",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                false,
                0,
                false,
                ""
                );

            Inicio(cmd2);
            bus.SendCommand(cmd2);
            Fim(cmd2);

            var cmd3 = new ExcluirEventoCommand(Guid.NewGuid());
            Inicio(cmd3);
            bus.SendCommand(cmd3);
            Fim(cmd3);

            Console.ReadKey();
        }

        private static void Inicio(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Inicio do Commando " + message.MessageType);
        }

        private static void Fim(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Fim do Commando " + message.MessageType);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*********************");
            Console.WriteLine("");
        }
    }

    public class FakeBus : IBus
    {
        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            Publish(theEvent);
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Comando " + theCommand.MessageType + " Lançado");
            Publish(theCommand);
        }

        private static void Publish<T>(T message) where T : Message
        {
            var msgType = message.MessageType;

            if (msgType.Equals("DomainNotification"))
            {
                var obj = new DomainNotificationHandler();
                ((IDomainNotificationHandler<T>)obj).Handle(message);
            }


            if (msgType.Equals("RegistrarEventoCommand") ||
              msgType.Equals("AtualizarEventoCommand") ||
              msgType.Equals("ExcluirEventoCommand"))
            {
                var obj = new EventoCommandHandler(
                    new FakeEventoRepository(),
                    new FakeUow(),
                    new FakeBus(),
                    new DomainNotificationHandler());
                ((IHandler<T>)obj).Handle(message);
            }

            if (msgType.Equals("EventoRegistradoEvent") ||
              msgType.Equals("EventoAtualizadoEvent") ||
              msgType.Equals("EventoExcluidoEvent"))
            {
                var obj = new EventoEventHandler();
                ((IHandler<T>)obj).Handle(message);
            }
        }
    }

    public class FakeEventoRepository : IEventoRepository
    {
        public void Add(Evento obj)
        {
            //
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //
        }

        public IEnumerable<Evento> Find(Expression<Func<Evento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> GetAll()
        {
            throw new NotImplementedException();
        }

        public Evento GetById(Guid id)
        {
            return new Evento("Fake", DateTime.Now, DateTime.Now, true, 0, true, "Empresa");
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            //throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Evento obj)
        {
            //
        }
    }

    public class FakeUow : IUnitOfWork
    {
        public CommandResponse Commit()
        {
            return new CommandResponse(true);
        }
    }
}