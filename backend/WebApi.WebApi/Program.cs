using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebApi.WebApi.Database;

var builder = WebApplication.CreateBuilder(args);

//configure route and for rest apis route should start with lower case
builder.Services.Configure<RouteOptions>(options =>{
     options.LowercaseUrls=true;
     });
    //configuring security for swagger documentation
    builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
       Description = "bearer token authentication",
       Name = "Authentication",
       In = ParameterLocation.Header
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
}); 

builder.Services.AddDbContext<DatabaseContext>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
