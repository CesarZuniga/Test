using test.api.Models;

namespace test.api.Repositories
{
    public interface ITipoMovimientoRepository
    {
        IQueryable<TipoMovimiento> ReadsItems();
    }
}