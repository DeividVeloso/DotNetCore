using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.EventHandlers
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>
    {
        public void Handle(EventoRegistradoEvent message)
        {
            //Enviar um e-mail ou criar um log!
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            //Enviar um e-mail ou criar um log!
        }

        public void Handle(EventoExcluidoEvent message)
        {
            //Enviar um e-mail ou criar um log!
        }
    }
}
