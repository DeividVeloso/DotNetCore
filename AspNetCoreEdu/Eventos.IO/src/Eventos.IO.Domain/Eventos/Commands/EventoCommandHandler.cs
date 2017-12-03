using Eventos.IO.Domain.CommandHandlers;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Eventos.Events;
using Eventos.IO.Domain.Interfaces;
using System;

namespace Eventos.IO.Domain.Eventos.Commands
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>,
        IHandler<AtualizarEventoCommand>,
        IHandler<ExcluirEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public EventoCommandHandler(
            IEventoRepository eventoRepository,
            IUnitOfWork uow, IBus bus,
            IDomainNotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _eventoRepository = eventoRepository;
            _bus = bus;
            _notifications = notifications;
        }

        public void Handle(RegistrarEventoCommand message)
        {
            var endereco = new Endereco(message.Endereco.Id, message.Endereco.Logradouro, message.Endereco.Numero, message.Endereco.Complemento, message.Endereco.Bairro, message.Endereco.CEP, message.Endereco.Cidade, message.Endereco.Estado, message.Endereco.EventoId.Value);
            var evento = Evento.EventoFactory.NovoEventoCompleto(
                  message.Id,
                  message.DescricaoCurta,
                  message.DescricaoLonga,
                  message.Nome,
                  message.DataIncio,
                  message.DataFim,
                  message.Gratuito,
                  message.Valor,
                  message.Online,
                  message.NomeEmpresa,
                  message.OrganizadorId,
                  endereco,
                  message.CategoriaId
                );

            if (!EventoValido(evento)) return;

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
            _eventoRepository.Adicionar(evento);
        }

        public void Handle(AtualizarEventoCommand message)
        {
            /*
             Valida se o evento pertence a pessoa que está editando
             */
            var eventoAtual = _eventoRepository.ObterPorId(message.Id);
            //Recebe um Id e o nome da classe
            if (!EventoExiste(message.Id, message.MessageType)) return;


            var evento = Evento.EventoFactory.NovoEventoCompleto(
                  message.Id,
                  message.DescricaoCurta,
                  message.DescricaoLonga,
                  message.Nome,
                  message.DataIncio,
                  message.DataFim,
                  message.Gratuito,
                  message.Valor,
                  message.Online,
                  message.NomeEmpresa,
                  message.OrganizadorId,
                  eventoAtual.Endereco, //Usa o endereço que já estava cadastrado.
                  message.CategoriaId
                );

            if (!EventoValido(evento)) return;

            _eventoRepository.Atualizar(evento);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoAtualizadoEvent(
                                    evento.Id,
                                    evento.DescricaoCurta,
                                    evento.DescricaoLonga,
                                    evento.Nome,
                                    evento.DataIncio,
                                    evento.DataFim,
                                    evento.Gratuito,
                                    evento.Valor,
                                    evento.Online,
                                    evento.NomeEmpresa));
            }

        }

        public void Handle(ExcluirEventoCommand message)
        {
            //Recebe um Id e o nome da classe
            if (!EventoExiste(message.Id, message.MessageType)) return;
            _eventoRepository.Remover(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoExcluidoEvent(message.Id));
            }
        }

        private bool EventoValido(Evento evento)
        {
            if (evento.EhValido()) return true;

            NotificarValidacoesErro(evento.ValidationResult);
            return false;
        }

        private bool EventoExiste(Guid id, string messageType)
        {
            var evento = _eventoRepository.ObterPorId(id);

            if (evento != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado"));
            return false;
        }
    }
}
