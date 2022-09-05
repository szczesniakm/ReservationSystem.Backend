using Newtonsoft.Json;
using ReservationSystem.Application.Exceptions;
using System.Net;

namespace ReservationSystem.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            } 
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var exceptionType = exception.GetType();

            switch (exception)
            {
                case ServiceException e when exceptionType == typeof(ServiceException):
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            var response = new { message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "applicatioin/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(payload);
        }
    }
}
