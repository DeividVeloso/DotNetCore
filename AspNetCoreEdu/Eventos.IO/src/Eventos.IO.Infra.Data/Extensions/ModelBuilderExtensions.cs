using Microsoft.EntityFrameworkCore;

namespace Eventos.IO.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        //Criando um método de extensão para aparecer no modelBuilder e eu conseguir criar 
        //um MAP das Entidades DBSet
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, 
            EntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
