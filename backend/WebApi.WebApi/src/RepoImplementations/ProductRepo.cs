using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        private readonly DbSet<Product> _products;
        private readonly DatabaseContext _dbcontext;
        public ProductRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _products = dbContext.Products;
            _dbcontext = dbContext;
        }
        public async Task<Product?> FindProductForUpdate(Guid id)
        {
            return await _products.FindAsync(id);
        }
        public async Task<Product> UpdateOne(Guid id, Product updated)
        {
            var existingProduct = await _products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found");
            }
            existingProduct.Title = updated.Title ?? existingProduct.Title;
            existingProduct.Price = updated.Price;
            existingProduct.Description = updated.Description ?? existingProduct.Description;
            existingProduct.Quantity = updated.Quantity;
            _dbcontext.SaveChanges();
            return updated;
        }
        public async Task<string> FindOneById(Guid id)
        {
            try
            {
                var found = await _products.FindAsync(id);
                return "found";
            }
            catch (Exception ex)
            {
                return "ex.message";
            }
        }
    }
}