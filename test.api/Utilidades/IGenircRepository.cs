using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using test.api.Data;

namespace test.api.Utilidades
{
    public interface IGenircRepository<TEntity> where TEntity : class
    {
        TestContext Context { get; set; }

        Task<TEntity> CreateItemAsync(TEntity item);
        IQueryable<TEntity> ReadsItems();
        IQueryable<TEntity> ReadsItems(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateItemAsync(TEntity item, Expression<Func<TEntity, bool>> expression);
    }
}