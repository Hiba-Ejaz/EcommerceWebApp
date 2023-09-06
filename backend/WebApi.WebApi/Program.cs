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
// using Serilog;
// using Serilog.Events;
// using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WebApi.WebApi.src.Database;
using Npgsql;
using WebApi.Domain.src.Entities;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApi.WebApi.src.Middleware;

var builder = WebApplication.CreateBuilder(args);
var timeStampInterceptor = new TimeStampInterceptor();
var npgsqlBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("DefaultConnection")
);
npgsqlBuilder.MapEnum<Role>();
npgsqlBuilder.MapEnum<OrderStatus>();
var dataSource = npgsqlBuilder.Build();

builder.Services.AddSingleton(npgsqlBuilder);
builder.Services.AddSingleton(timeStampInterceptor);

builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Configure Serilog for logging
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Debug()
//     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//     .Enrich.FromLogContext()
//     .WriteTo.Console() // Configure Serilog to output log messages to the console
//     .CreateLogger();

// builder.Logging.AddSerilog();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
//builder.Services.AddScoped(typeof(IBaseRepo<Category>), typeof(BaseRepo<Category>));
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartItemsRepo, CartItemRepo>();
// builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();
//builder.Services.AddScoped(typeof(IBaseService<Category>), typeof(BaseService<Category>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        options.AddInterceptors(timeStampInterceptor);
        options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
    },
    ServiceLifetime.Scoped
);

 builder.Services.AddControllers();

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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmailAllowedList", policy => policy.RequireClaim(ClaimTypes.Email, "hibaejaz@gmail.com"));
});

//builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ErrorHandlerMiddleware>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
// CORS configuration
var allowOrigins = "allowOrigins";
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000","https://shopnshop.netlify.app","http://localhost:3001", "http://localhost:3003", "http://localhost:3002")
           .AllowAnyHeader()
           .AllowAnyMethod();
});
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
// dataSource.Dispose();
