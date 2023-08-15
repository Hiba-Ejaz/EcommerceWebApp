using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Domain.src.Entities;



using Microsoft.Extensions.Configuration;
using WebApi.WebApi.src.Database;

namespace WebApi.WebApi.Database
{
    public class DatabaseContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions options,IConfiguration configuration):base(options)
        {
            _configuration = configuration;
        }
        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

       public DbSet<Product> Products { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Order> Orders{ get; set; }
       public DbSet<OrderItem> OrderItems   { get; set; }
       public DbSet<Image> Images{ get; set; } 

               protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey("OrderId","ProductId");
            modelBuilder.Entity<User>().HasIndex(u=>u.Email).IsUnique();
             modelBuilder.HasPostgresEnum<Role>(); 
            base.OnModelCreating(modelBuilder);
        }

      






           protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            var builder=new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
             builder.MapEnum<Role>(); 
            optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
            optionsBuilder.AddInterceptors(new TimeStampInterceptor());
        }
    }
}