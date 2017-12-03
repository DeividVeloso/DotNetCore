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
                ps.AddProfile(new DomainToViewModelMappingProfile());
                //ViewModels para Commandos
                ps.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
    

   
}
