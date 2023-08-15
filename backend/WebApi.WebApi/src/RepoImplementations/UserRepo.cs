
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class UserRepo:BaseRepo<User>,IUserRepo
    {
        private readonly DbSet<User> _users;
        private readonly DatabaseContext _dbcontext;
        public UserRepo(DatabaseContext dbContext):base(dbContext)
        {
            _users=dbContext.Users;
            _dbcontext=dbContext;
        }

        public async Task<User> CreateAdmin(User user)
        {
           user.Role=Role.Admin;
          await _users.AddAsync(user);
           await _dbcontext.SaveChangesAsync();
           return user;
        }
      
        public async Task<User?> FindUserByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email.Contains(email));
        }

        public async Task<User> UpdatePassword(User user)
        {
          _users.Update(user);
         await _dbcontext.SaveChangesAsync();
         return user;
        }
         public override Task<User> CreateOne(User entity)
        {
            entity.Role=Role.Customer;
           return base.CreateOne(entity);
        }

        
       
    }
}