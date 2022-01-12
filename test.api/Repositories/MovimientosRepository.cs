using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using test.api.Models;
using test.api.Utilidades;

namespace test.api.Repositories
{
    public class MovimientosRepository : IMovimientosRepository
    {


        private IGenircRepository<Movimientos> _geneRepo;
        /// <summary>
        /// ctor
        /// </summary>
        /// <summary>
        /// ctor
        /// </summary>
        public MovimientosRepository(IGenircRepository<Movimientos> geneRepo)
        {
            _geneRepo = geneRepo;
        }


        public async Task<Movimientos> CreateItemAsync(Movimientos item)
        {
            using (var transaction = await _geneRepo.Context.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead))
            {
                var lastItem = await _geneRepo.ReadsItems().OrderByDescending(x => x.MovimientoId).FirstOrDefaultAsync();
                int id = lastItem == null ? 1 : lastItem.MovimientoId + 1;
                item.MovimientoId = id;
                await _geneRepo.Context.Movimientos.AddAsync(item);
                await _geneRepo.Context.SaveChangesAsync();
                Cuentas cuenta = await _geneRepo.Context.Cuentas.FirstAsync(c => c.CuentaId == item.CuentaId);
                cuenta.SaldoActual = item.TipoId == 1 ? cuenta.SaldoActual + item.Monto : cuenta.SaldoActual - item.Monto;
                _geneRepo.Context.Cuentas.Update(cuenta);
                await _geneRepo.Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return item;
        }

        public IQueryable<MovimientosBO> ReadsItems() => ReadsItems(t => true);

        public IQueryable<MovimientosBO> ReadsItems(Expression<Func<MovimientosBO, bool>> expression)
        {
            return (from m in _geneRepo.ReadsItems()
                    join t in _geneRepo.Context.TipoMovimientos on m.TipoId equals t.TipoId
                    join cu in _geneRepo.Context.Cuentas on m.CuentaId equals cu.CuentaId
                    join ci in _geneRepo.Context.Clientes on cu.ClienteId equals ci.ClienteId
                    select new MovimientosBO
                    {
                        MovimientoId = m.MovimientoId,
                        Monto = m.Monto,
                        CuentaId = m.CuentaId,
                        TipoId = m.TipoId,
                        Tipo = t.Descripcion,
                        NumeroCuenta = cu.NumeroCuenta,
                        Nombre = ci.Nombre
                    }).Where(expression).AsQueryable();
        }


    }
}
