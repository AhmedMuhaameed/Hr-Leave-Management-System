using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagment.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExeptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            object problem;
            switch (ex)
            {
                case LeaveManagment.Application.Exceptions.BadRequestException badRequestException:
                    statusCode= HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type =  nameof(LeaveManagment.Application.Exceptions.BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case Application.Exceptions.NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Detail = NotFound.InnerException?.Message,
                        Type = nameof(Application.Exceptions.NotFoundException),
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    };
                    break;
            }
            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogError(logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem); 

        }
    }
}
