using SIMFranchise.Wrappers;
using System.Net;
using System.Text.Json;

namespace SIMFranchise.Middlewares
{
    namespace SIMFranchise.Middlewares
    {
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;

            public ExceptionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);  
                }
                catch (Exception ex)
                { 
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = ApiResponse<string>.FailureResponse("Server Error.", new List<string> { exception.Message });

                var json = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }
        }
    }
}
