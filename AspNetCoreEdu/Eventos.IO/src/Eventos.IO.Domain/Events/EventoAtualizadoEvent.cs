using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Events
{
    public class EventoAtualizadoEvent : BaseEventoEvent
    {
        public EventoAtualizadoEvent(
           Guid id,
           string nome,
           string descricaoCurta,
           string descricaoLonga,
           DateTime dataInicio,
           DateTime dataFim,
           bool gratuito,
           decimal valor,
           bool online,
           string nomeEmpresa)
        {
            Id = id;
            DescricaoCurta = descricaoCurta;
            DescricaoLonga = descricaoLonga;
            Nome = nome;
            DataIncio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;

            //Persistir para mensagem
            AggregateId = id;
        }
    }
}
