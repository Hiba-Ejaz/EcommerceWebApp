
using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Dtos;
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
             _products=dbContext.Products;
            _dbcontext=dbContext;
        }
        public async Task<Product?> FindProductForUpdate(Guid id)
        {
           return await _products.FindAsync(id);
        }
    }
}