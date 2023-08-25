
using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Implementations.Shared;

namespace WebApi.WebApi.src.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           try{
            await next(context);
           }
           catch(CustomException ex){
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsync(ex.ErrorMessage);
           }
            catch(DbUpdateException ex){
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.InnerException!.Message);
           }
           catch(Exception ex){
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error");
           }
        }
    }
}