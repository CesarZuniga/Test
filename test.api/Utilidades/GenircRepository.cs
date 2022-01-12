using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using test.api.Data;

namespace test.api.Utilidades
{
    public class GenircRepository<TDbContext, TEntity> : IGenircRepository<TEntity> where TDbContext : TestContext, new() where TEntity : class
    {
        /// <summary>
        /// ctor 
        /// </summary>
        public GenircRepository() { }
        /// <summary>
        /// ctor
        /// </summary>
        public GenircRepository(DbContextOptions<TDbContext> options)
        {
            _entities = (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
        }
        /// <summary>
        ///  Private Instance of DbContext
        /// </summary>
        private TestContext _entities = new TDbContext();
        /// <summary>
        /// Public DB Context property
        /// </summary>
        public TestContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        /// <summary>
        /// Reads all items
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> ReadsItems()
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>();
            return query;
        }
        /// <summary>
        /// Reads items by expreInvon
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> ReadsItems(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>().Where(expression);
            return query;
        }
        /// <summary>
        /// Create item async
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TEntity> CreateItemAsync(TEntity item)
        {
            // Initialize transaction
            using (var transaction = await _entities.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead))
            {
                // Add item into entity
                _entities.Set<TEntity>().Add(item);
                // Save changes 
                await _entities.SaveChangesAsync();
                //  commit transaction
                transaction.Commit();
            }
            return item;
        }
        /// <summary>
        ///  update item async
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateItemAsync(TEntity item, Expression<Func<TEntity, bool>> expression)
        {
            using (var transaction = await _entities.Database
              .BeginTransactionAsync(IsolationLevel.RepeatableRead))
            {
                TEntity original = await _entities.Set<TEntity>().FirstOrDefaultAsync(expression);
                var originalEntry = _entities.Entry(original);
                foreach (var property in originalEntry.Metadata.GetProperties())
                {
                    //Set current value and original value to verify if not are equals
                    // if not are equals set original value from current value                                                 
                    var proposedValue = item.GetType()
                      .GetProperty(property.Name).GetValue(item, null);
                    var originalValue = originalEntry
                      .Property(property.Name).OriginalValue;
                    if (proposedValue != null)
                        if (!proposedValue.Equals(originalValue))
                            original.GetType().GetProperty(property.Name)
                              .SetValue(original, proposedValue);
                }
                // Save changes 
                await _entities.SaveChangesAsync();
                // commit transaction
                transaction.Commit();
            }
            return item;
        }
    }
}
