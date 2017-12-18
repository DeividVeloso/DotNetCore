using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
namespace Eventos.IO.Infra.Data.Repository
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(EventosContext context) : base(context)
        {

        }

        public override IEnumerable<Evento> ObterTodos()
        {
            var sql = "SELECT * FROM Eventos e where e.excluido = 0 order by e.datafim desc";
            //Pega o meu DbSet a Conexão com o banco e usa o Query do Dapper para executar a query.
            return Db.Database.GetDbConnection().Query<Evento>(sql);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            //o Db vem do Repository.
            Db.Enderecos.Add(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Db.Enderecos.Update(endereco);
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            return Db.Enderecos.Find(id);
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            var sql = @"SELECT * FROM EVENTOS E WHERE E.Excluido = 0 AND E.OrganizadorId = @oid ORDER BY E.datafim desc";
            return Db.Database.GetDbConnection().Query<Evento>(sql, new { oid = organizadorId });
        }

        //Retorna o Evento com endereço INNER JOIN
        public override Evento ObterPorId(Guid id)
        {
            //o Id é passad como parâmetro no Where dessa forma para evitar o SQL Injection
            var sql = @"SELECT * FROM Eventos E LEFT JOIN Enderecos EN ON E.Id = En.EventoId WHERE E.Id = @uid";
            //Query = Retorna um Evento e Endereço e no final vai ser um Evento
            var evento = Db.Database.GetDbConnection().Query<Evento, Endereco, Evento>(sql,
                //Delegate, se o endereço for diferente de Null desse evento
                //Então atribui o endereço ao evento.
                (evto, ender) =>
                {
                    if (ender != null)
                        evto.AtribuirEntereco(ender);
                    return evto;
                }, new { uid = id }); //Where do Dapper

            return evento.FirstOrDefault();
        }
    }
}
