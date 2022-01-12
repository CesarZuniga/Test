using System.Linq.Expressions;
using test.api.Models;

namespace test.api.Repositories
{
    public interface IMovimientosRepository
    {
        Task<Movimientos> CreateItemAsync(Movimientos item);
        IQueryable<MovimientosBO> ReadsItems();
        IQueryable<MovimientosBO> ReadsItems(Expression<Func<MovimientosBO, bool>> expression);
    }
}