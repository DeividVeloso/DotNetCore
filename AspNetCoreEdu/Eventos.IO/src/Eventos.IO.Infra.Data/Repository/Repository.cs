using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Eventos.IO.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {

        protected EventosContext Db;//Nosso contexto
        protected DbSet<TEntity> DbSet; //Seta a entidade no nosso contexto

        protected Repository(EventosContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        //Vamos colocar todos os métodos como virtual
        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            //Desliga o tracking dessa consulta, performance e evita problemas de 
            //entidades trackeados durante o processo de update
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            //Esse ID vem de Entity, por isso consigo comparar, pois é uma classe generica.
            return DbSet.AsNoTracking().FirstOrDefault(w => w.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            //Cuidado, esse ToList retorna a tabela inteira
            //o aconselhavel é override desse método na minha classe especifica e trabalhar com paginação.
            return DbSet.ToList();
        }

        public virtual void Remover(Guid id)
        {

            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            //Ele retorna sempre um int por que é o número de linhas afetadas no banco.
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            //Acaba com a instancia do contexto
            Db.Dispose();
        }
    }
}
