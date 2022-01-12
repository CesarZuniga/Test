using System.Linq.Expressions;
using test.api.Data;
using test.api.Models;

namespace test.api.Repositories
{
    public interface IClientesRepository
    {
        Task<Clientes> CreateItemAsync(Clientes item);
        IQueryable<Clientes> ReadsItems();
        IQueryable<Clientes> ReadsItems(Expression<Func<Clientes, bool>> expression);
        Task<Clientes> UpdateItemAsync(Clientes item, Expression<Func<Clientes, bool>> expression);
    }
}