
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Implementations.Shared;

namespace WebApi.WebApi.src.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        public ErrorHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           try{
            await next(context);
           }
           catch(CustomException ex){
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsync(ex.ErrorMessage);
            await HandleException(context, ex,_logger);
           }
            catch(DbUpdateException ex){
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.InnerException!.Message);
              await HandleException(context, ex,_logger);
           }
           catch(Exception ex){
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error");
             await HandleException(context, ex,_logger);
           }
        }
         public static Task HandleException(HttpContext context, Exception ex,ILogger logger){
            int statusCode=(int)HttpStatusCode.InternalServerError;
            var errorResponse=new ErrorResponse{
                StatusCode=statusCode,
            ErrorMessage=ex.Message
            };
            context.Response.StatusCode = statusCode;
            context.Response.ContentType="application/json";
             logger.LogError($"An exception of type {ex.GetType()} occurred: {ex.Message}");
            return context.Response.WriteAsync(errorResponse.ToString());
         }
    }
}