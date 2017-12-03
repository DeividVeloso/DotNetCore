using AutoMapper;
using Eventos.IO.Application.ViewModels;
using Eventos.IO.Domain.Eventos.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EventoViewModel, RegistrarEventoCommand>()
                //Esses dados de c => é do EventoViewModel
                .ConstructProjectionUsing(c => new RegistrarEventoCommand(
                    c.Nome,
                    c.DescricaoCurta,
                    c.DescricaoLonga,
                    c.DataInicio,
                    c.DataInicio,
                    c.Gratuito,
                    c.Valor,
                    c.Online,
                    c.NomeEmpresa,
                    c.OrganizadorId,
                    c.CategoriaId,
                    new IncluirEnderecoCommand(c.Endereco.Id,
                                                c.Endereco.Logradouro,
                                                c.Endereco.Numero,
                                                c.Endereco.Complemento,
                                                c.Endereco.Bairro,
                                                c.Endereco.CEP,
                                                c.Endereco.Cidade,
                                                c.Endereco.Estado,
                                                c.Id)
                    ));



            CreateMap<EventoViewModel, AtualizarEventoCommand>()
                .ConstructUsing(c => new AtualizarEventoCommand(c.Id, c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId));

            CreateMap<EventoViewModel, ExcluirEventoCommand>()
                .ConstructUsing(c => new ExcluirEventoCommand(c.Id));

            //CreateMap<EnderecoViewModel, AtualizarEnderecoEventoCommand>()
            //  .ConstructUsing(c => new AtualizarEnderecoEventoCommand(Guid.NewGuid(), c.Logradouro, c.Numero, c.Complemento, c.Bairro, c.CEP, c.Cidade, c.Estado, c.EventoId));
            //// Organizador
            //CreateMap<OrganizadorViewModel, RegistrarOrganizadorCommand>()
            //    .ConstructUsing(c => new RegistrarOrganizadorCommand(c.Id, c.Nome, c.CPF, c.Email));
        }
    }
}
