using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GoalCalendar.Utilities.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            var response = context.Response;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Unexpected error";
            var description = "Unexpected error";

            if (exception is BaseHttpException customException)
            {
                message = customException.Message;
                description = customException.Description;
                statusCode = customException.Code;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            return response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorResponse
            {
                Message = message,
                Description = description,
            }));
        }
    }
}
