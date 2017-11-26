using AutoMapper;
using Eventos.IO.Application.ViewModels;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Organizadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                //Commandos para ViewModels
                ps.AddProfile(new DomainToViewModelMappingProfile);
                //ViewModels para Commandos
                ps.AddProfile(new ViewModelToDomainMappingProfile);
            });
        }
    }
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Evento, EventoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EventoViewModel, RegistrarEventoCommand>()
                //Esses dados de c => é do EventoViewModel
                .ConstructProjectionUsing(c => new RegistrarEventoCommand(
                    c.Nome, 
                    c.DataInicio, 
                    c.DataInicio,
                    c.Gratuito,
                    c.Valor,
                    c.Online,
                    c.NomeEmpresa
                    ));
        }
    }
}
