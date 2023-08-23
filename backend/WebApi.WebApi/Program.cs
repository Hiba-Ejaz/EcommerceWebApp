using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Implementations;
using WebApi.Business.src.Implementations.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.WebApi.Database;
using WebApi.WebApi.src.RepoImplementations;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Configure Serilog to output log messages to the console
    .CreateLogger();

builder.Logging.AddSerilog();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
//builder.Services.AddScoped(typeof(IBaseRepo<Category>), typeof(BaseRepo<Category>));
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
 builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartItemsRepo, CartItemRepo>();
// builder.Services.AddScoped<IOrderRepo, OrderRepo>();
// builder.Services.AddScoped<IOrderItemsRepo, OrderItemRepo>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();
//builder.Services.AddScoped(typeof(IBaseService<Category>), typeof(BaseService<Category>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();

// Configure route and for REST APIs route should start with lowercase
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Configure security for Swagger documentation
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authentication",
        In = ParameterLocation.Header
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Configure authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Security:JwtKey"];
    jwtKey = string.IsNullOrEmpty(jwtKey) ? "default-key" : jwtKey;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Security:Issuer"],
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//
// CreateAdminUser(app);
// ...
// Method to create the admin user
// void CreateAdminUser(IHost app)
// {
//     using (var serviceScope = app.Services.CreateScope())
//     {
//         var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
//         var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
//         var userRepo = serviceScope.ServiceProvider.GetRequiredService<IUserRepo>();
//         var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
        // Extract the admin information from the connection string
       
    //     string GetConnectionStringValue(string connectionString, string key)
    //                 {
    // var keyValuePairs = connectionString.Split(';')
    //     .Select(part => part.Split('='))
    //     .Where(split => split.Length == 2)
    //     .ToDictionary(split => split[0], split => split[1], StringComparer.OrdinalIgnoreCase);
    // if (keyValuePairs.TryGetValue(key, out var value))
    // {
    //     Log.Information("Value  Value of value: {Value}", value);
    //     return value;
    // }
//     return null; // Key not found
// }
    //    var connectionString = configuration.GetConnectionString("DefaultConnection");
    //     var adminUsername = GetConnectionStringValue(connectionString, "Username");
    //     var adminPassword = GetConnectionStringValue(connectionString, "Password");
    //     // Check if the admin user exists, create if not
    //    var adminUser = userRepo.FindUserByEmail(adminUsername).Result;
        //  if (adminUser == null)
        // {
            // Create the admin user with the "Admin" role
            //  PasswordService.HashPassword("hiba1234",out string hashedpassword,out Byte[] salt);   
            // if (hashedpassword != null){
            //     Log.Information("Value  Value of hashedpassword: {Value}", hashedpassword);
            //     }
            //      if (salt != null){
            //     Log.Information("Value  Value of salt: {Value}", salt);
            //     }
            // var admin = new User
            // { 
            //     Email = "hibaejaz@gmail.com",
            //     Role = Role.Admin,
            //     Name="hiba",
            //     Avatar="avatar",
            //     Password=hashedpassword,
            //     Salt=salt,
            //      // Set the role to "Admin"
            //     // Set other properties as needed for the User entity
            // };
            // // Log.Information("Value  Value of value: {Value}", value);
            // //Log.Information("USERnAME.",adminUsername);
            // var createdAdmin = userService.CreateAdmin(admin).Result;
            // if(createdAdmin.Email!=null){
            //     Log.Information("{createdAdmin email}", createdAdmin.Email);
            // }
            // else{
            //     Log.Information("created admin email null");
            // }
            // if (createdAdmin != null)
            // {
            //     var cn=createdAdmin.Email;
            //     var cn2=createdAdmin.Avatar;
            //     var cn3=createdAdmin.Role;
            //     var cn4=createdAdmin.Name;
            //     Log.Information("email avatar role name {cn} {cn2} {cn3} {cn4}",cn,cn2,cn3,cn4);
            // }
   //}
//}
 void ConfigureLogging(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole(); // Add Console logging provider
            // You can add other logging providers here, such as Debug, EventLog, etc.
        });
    }
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// CORS configuration
var allowOrigins = "allowOrigins";
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000","http://localhost:3001","http://localhost:3003", "http://localhost:3002")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthorization();
app.MapControllers();
app.Run();
