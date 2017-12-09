using Eventos.IO.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Notifications;

namespace Eventos.IO.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IBus
    {
        //Vamos usar esse delegate para acessar nossa IoC
        public static Func<IServiceProvider> ContainerAccessor { get; set; }
        //Executa e recebe o ContainerAccessor.
        private static IServiceProvider Container => ContainerAccessor();

        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            Publish(theEvent);
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            Publish(theCommand);
        }

        //Como vou pegar uma instancia de objeto se ele é genérico???? 
        //Como identificar no IoC? Passo uma Interface retorna uma Classe????
        private static void Publish<T>(T message) where T : Message
        {
            if (Container == null) return;
            //Pega o GetService e verifica se a mensagem que eu estou passando é do tipo DomainNotification
            //Se for eu troco o tipo da instancia que estou recebendo para IDomainNotification ou IHandle
            //GetService Mecanismo de DI
            var obj = Container.GetService(message.MessageType.Equals("DomainNotification")
                                ? typeof(IDomainNotificationHandler<T>)
                                : typeof(IHandler<T>));

            //Pegando e convertendo explicitamente
            ((IHandler<T>)obj).Handle(message);

        }
    }
}
