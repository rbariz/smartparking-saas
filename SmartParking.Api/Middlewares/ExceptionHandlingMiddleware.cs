using SmartParking.Application.Common;
using SmartParking.Domain.Common;
using System.Net;

namespace SmartParking.Api.Middlewares
{
   

    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await WriteProblemAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConflictException ex)
            {
                await WriteProblemAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (ValidationException ex)
            {
                await WriteProblemAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (DomainException ex)
            {
                await WriteProblemAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private static async Task WriteProblemAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string detail)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                title = statusCode.ToString(),
                status = (int)statusCode,
                detail
            };

            await context.Response.WriteAsJsonAsync(payload);
        }
    }
}
