using AutoMapper;
using Eventos.IO.Application.Interfaces;
using Eventos.IO.Application.Services;
using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Eventos.Events;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Infra.Data.Context;
using Eventos.IO.Infra.Data.Repository;
using Eventos.IO.Infra.Data.Uow;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application
            services.AddSingleton(Mapper.Configuration);//Vai ser singleton as configurações do AutoMapper
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetRequiredService));
            services.AddScoped<IEventoAppService, EventoAppService>();

            // Domain - Commands
            services.AddScoped<IHandler<RegistrarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<AtualizarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<ExcluirEventoCommand>, EventoCommandHandler>();

            // Domain - Eventos
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IHandler<EventoRegistradoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EventoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EventoExcluidoEvent>, EventoEventHandler>();

            // Infra - Data
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EventosContext>();
        }
    }
}
