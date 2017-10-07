using Eventos.IO.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T: Message
    {
        //Método para vefificar se tem notificação no dominio
        //Houve um problema?
        bool HasNotifications();
        List<T> GetNotifications();
    }
}
