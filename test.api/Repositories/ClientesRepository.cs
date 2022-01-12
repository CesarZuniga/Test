using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using test.api.Models;
using test.api.Utilidades;

namespace test.api.Repositories
{
    public class ClientesRepository : IClientesRepository
    {


        private IGenircRepository<Clientes> _geneRepo;
        /// <summary>
        /// ctor
        /// </summary>
        /// <summary>
        /// ctor
        /// </summary>
        public ClientesRepository(IGenircRepository<Clientes> geneRepo)
        {
            _geneRepo = geneRepo;
        }


        public async Task<Clientes> CreateItemAsync(Clientes item)
        {
            var lastItem = await _geneRepo.ReadsItems().OrderByDescending(x => x.ClienteId).FirstOrDefaultAsync();
            int id = lastItem == null ? 1 : lastItem.ClienteId + 1;
            item.ClienteId = id;
            await _geneRepo.CreateItemAsync(item);
            return item;
        }

        public IQueryable<Clientes> ReadsItems() => _geneRepo.ReadsItems();

        public IQueryable<Clientes> ReadsItems(Expression<Func<Clientes, bool>> expression) => _geneRepo.ReadsItems(expression);

        public Task<Clientes> UpdateItemAsync(Clientes item, Expression<Func<Clientes, bool>> expression) => _geneRepo.UpdateItemAsync(item, expression);


    }
}
