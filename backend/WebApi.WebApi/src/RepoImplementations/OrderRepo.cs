
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class OrderRepo : BaseRepo<Order>,IOrderRepo
    {
        private readonly DbSet<Order> _products;
        private readonly DatabaseContext _dbcontext;

        public OrderRepo(DatabaseContext dbContext) : base(dbContext)
        {
             _products=dbContext.Orders;
            _dbcontext=dbContext;
        }
    
        
    }
}