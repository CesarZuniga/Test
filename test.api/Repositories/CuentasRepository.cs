using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using test.api.Models;
using test.api.Utilidades;

namespace test.api.Repositories
{
    public class CuentasRepository : ICuentasRepository
    {

        private IGenircRepository<Cuentas> _geneRepo;
        /// <summary>
        /// ctor
        /// </summary>
        /// <summary>
        /// ctor 
        /// </summary>
        public CuentasRepository(IGenircRepository<Cuentas> geneRepo)
        {
            _geneRepo = geneRepo;
        }


        public async Task<Cuentas> CreateItemAsync(Cuentas item)
        {
            var lastItem = await _geneRepo.ReadsItems().OrderByDescending(x => x.CuentaId).FirstOrDefaultAsync();
            int id = lastItem == null ? 1 : lastItem.CuentaId + 1;
            item.CuentaId = id;
            await _geneRepo.CreateItemAsync(item);
            return item;
        }

        public IQueryable<CuentasBO> ReadsItems() => ReadsItems(t => true);

        public IQueryable<CuentasBO> ReadsItems(Expression<Func<CuentasBO, bool>> expression)
        {
            return (from cu in _geneRepo.ReadsItems()
             join ci in _geneRepo.Context.Clientes on cu.ClienteId equals ci.ClienteId
             select new CuentasBO
             {
                 ClienteId = cu.ClienteId,
                 CuentaId = cu.CuentaId,
                 NumeroCuenta = cu.NumeroCuenta,
                 SaldoActual = cu.SaldoActual,
                 Nombre = ci.Nombre

             }).Where(expression).AsQueryable();
        }
    }
}
