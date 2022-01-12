using test.api.Models;
using test.api.Utilidades;

namespace test.api.Repositories
{
    public class TipoMovimientoRepository : ITipoMovimientoRepository
    {

        private IGenircRepository<TipoMovimiento> _geneRepo;
        /// <summary>
        /// ctor
        /// </summary>
        /// <summary>
        /// ctor
        /// </summary>
        public TipoMovimientoRepository(IGenircRepository<TipoMovimiento> geneRepo)
        {
            _geneRepo = geneRepo;
        }
        public IQueryable<TipoMovimiento> ReadsItems() => _geneRepo.ReadsItems();

    }
}
