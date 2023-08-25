using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;
        public BaseRepo(DatabaseContext dbContext)
        {
            _databaseContext = dbContext;
            _dbSet = dbContext.Set<T>();

        }
        public virtual async Task<T> CreateOne(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteOneById(T entity)
        {
            _dbSet.Remove(entity);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        public virtual async Task<IEnumerable<T>> GetAll(SearchQueryOptions options)
        {
            IQueryable<T> query = _dbSet;
            Type entityType = typeof(T);
            string? SearchQuery = options.SearchQuery;
            bool SortAscending = options.SortAscending;
            string SortBy = options.SortBy;
            if (entityType is Product)
            {
                if (!string.IsNullOrWhiteSpace(options.SearchQuery))
                {
                    var collection = _dbSet as DbSet<Product>;
                    PropertyInfo? titleProperty = entityType.GetProperty("Title",
                     BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (titleProperty != null)
                    {
                        query = query.Where(entity => (entityType == typeof(Product)) &&
                     string.Equals(titleProperty.GetValue(collection).ToString(), options.SearchQuery, StringComparison.OrdinalIgnoreCase));
                    }
                }
            }
            //     PropertyInfo? priceProperty = entityType.GetProperty("Price",
            //        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            //     if (priceProperty != null)
            //     {
            //         // Sort the query based on the Price property and sort direction
            //         if (options.SortAscending)
            //         {
            //             query = query.OrderBy(entity => priceProperty.GetValue(entity));
            //         }
            //         else
            //         {
            //             query = query.OrderByDescending(entity => priceProperty.GetValue(entity));
            //         }
            //     }
            //     return await query.ToListAsync();
            // }
            // if(entityType is Product){
            //     var collection=_dbSet as DbSet<Product>;
            //       PropertyInfo? titleProperty = entityType.GetProperty("Title",
            //      BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            //      if(titleProperty != null){
            //    query = query.Where(entity => (entityType == typeof(User)) &&
            // string.Equals(titleProperty.GetValue(collection).ToString(), options.SearchQuery, StringComparison.OrdinalIgnoreCase));
            // }
            // }
            // if(entityType is Order){
            //     var collection=_dbSet as DbSet<Order>;
            // }
            // if(entityType is OrderItem){
            //     var collection=_dbSet as DbSet<OrderItem>;
            // }
            //  if(entityType is Category){
            //     var collection=_dbSet as DbSet<Category>;
            // }
            return await _dbSet.AsNoTracking().ToArrayAsync();  
        }

        public async Task<T?> GetOneById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}




