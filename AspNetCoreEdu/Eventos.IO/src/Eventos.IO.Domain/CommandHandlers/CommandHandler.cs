using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        public CommandHandler(IUnitOfWork uow, IBus bus)
        {
            _uow = uow;
            _bus = bus;
        }

        protected bool Commit()
        {
            //TODO: Validar se há alguma validação de negócio com erro!
            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            Console.WriteLine("Ocorreu um erro ao salvar os dados no banco");
            _bus.RaiseEvent();
            return false;
        }

        protected void Notificacoes(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
                _bus.RaiseEvent();
            }
        }
    }
}
