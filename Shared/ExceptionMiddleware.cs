using Services.B.Core.Dtos;
using Services.B.Core.Exceptions;
using Services.B.Core.Helpers;
using Services.B.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Services.B.Shared
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ServiceExceptions serviceExceptions, ILoggerRepo loggerRepo)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await serviceExceptions.HandleException(ex, loggerRepo);

                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                var handleException = await serviceExceptions.HandleException(ex, loggerRepo);

                await HandleExceptionAsync(httpContext, handleException);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            var details = new ValidationProblemDetails(ex.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Status = 400
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(details));
        }

        private Task HandleExceptionAsync(HttpContext context, ServiceResponseBase serviceResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(serviceResponse));
        }
    }

}