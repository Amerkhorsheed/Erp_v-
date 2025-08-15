using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Seller.API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // In a real app, you would log the exception here
            // logger.Error(context.Exception, "An unhandled API exception occurred.");

            // Create a user-friendly JSON response
            var errorResponse = new
            {
                success = false,
                message = "An unexpected error occurred on the server.",
                // For debugging, you might want to include the actual error.
                // In production, you would remove the 'detail' line.
                detail = context.Exception.Message
            };

            // Create the new HttpResponseMessage with a 500 status code
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                errorResponse
            );
        }
    }
}