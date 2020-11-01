using System;
using System.Net;
using System.Threading.Tasks;
using Framework.Domain.Exceptions;
using HomeCleaning.ApiAndAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeCleaning.ApiAndAuth
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public UnhandledExceptionFilterAttribute(ILogger<UnhandledExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }

        private static int _exceptionTracker = 1;


        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var actionDescriptor =
                (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) context.ActionDescriptor;
            Type controllerType = actionDescriptor.ControllerTypeInfo;

            var controllerBase = typeof(ControllerBase);
            var controller = typeof(Controller);

            if (controllerType.IsSubclassOf(controllerBase) && !controllerType.IsSubclassOf(controller))
            {
                string result;
                if (context.Exception is BusinessException exception)
                {
                    var result1 = new ErrorDetails()
                    {
                        ReferenceCode = "0",
                        Message = exception.Message
                    };
                    result = JsonConvert.SerializeObject(result1);

                    switch (context.Exception)
                    {
                        case DuplicateException _:
                            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;
                            break;
                        case BadDataException _:
                            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                            break;
                        case NotFoundException _:
                            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                            break;
                        case OperationNotAllowedException _:
                            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.PreconditionFailed;
                            break;
                        default:
                            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                            break;
                    }
                }
                else
                {
                    string exceptionTracker = DateTime.Today.Day.ToString() + _exceptionTracker;
                    _exceptionTracker++;
                    logger.LogError(context.Exception, $"Exception {exceptionTracker}");

                    var result1 = new ErrorDetails
                    {
                        ReferenceCode = exceptionTracker,
#if DEBUG
                        Message = context.Exception.ToString()
#else
                        Message = "خطایی در انجام درخواست پیش آمده"
#endif
                    };
                    result = JsonConvert.SerializeObject(result1);

                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                }

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.ContentLength = result.Length;
                await context.HttpContext.Response.WriteAsync(result);
            }
            else if (controllerType.IsSubclassOf(controllerBase) && controllerType.IsSubclassOf(controller))
            {
                // Handle page exception
            }
            else
            {
                await base.OnExceptionAsync(context);
            }
        }
    }
}