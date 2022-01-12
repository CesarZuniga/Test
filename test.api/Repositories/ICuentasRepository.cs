using System.Linq.Expressions;
using test.api.Models;

namespace test.api.Repositories
{
    public interface ICuentasRepository
    {
        Task<Cuentas> CreateItemAsync(Cuentas item);
        IQueryable<CuentasBO> ReadsItems();
        IQueryable<CuentasBO> ReadsItems(Expression<Func<CuentasBO, bool>> expression);
    }
}