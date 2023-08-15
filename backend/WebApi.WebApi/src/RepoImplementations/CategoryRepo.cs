using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        private readonly DbSet<Category> _products;
        private readonly DatabaseContext _dbcontext;

        public CategoryRepo(DatabaseContext dbContext) : base(dbContext)
        {
             _products=dbContext.Categories;
            _dbcontext=dbContext;
        }
    
        
    }
}