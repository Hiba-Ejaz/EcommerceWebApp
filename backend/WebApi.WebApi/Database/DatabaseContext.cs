using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Domain.src.Entities;
using Microsoft.Extensions.Configuration;

namespace WebApi.WebApi.Database
{
    public class DatabaseContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       public DbSet<Product> Products { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Order> Orders{ get; set; }
       public DbSet<OrderItem> OrderItems   { get; set; }
       public DbSet<Image> Images{ get; set; } 
           protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            var builder=new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseNpgsql(builder.Build());
        }
    }
}