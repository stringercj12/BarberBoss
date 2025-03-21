using BarberBoss.Communication.Responses;
using BarberBoss.Exceptions.Messages;
using BarberBoss.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BarberBossException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnkowError(context);
            }

        }

        private void HandleProjectException(ExceptionContext context)
        {
            var barberBossException = (BarberBossException)context.Exception;
            var errorResponse = new ResponseErrorJson(barberBossException.GetErrors());

            context.HttpContext.Response.StatusCode = barberBossException.StatusCode;
            context.Result = new ObjectResult(errorResponse);

        }

        private void ThrowUnkowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
