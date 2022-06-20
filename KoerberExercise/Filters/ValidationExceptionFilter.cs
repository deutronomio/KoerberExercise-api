using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace KoerberExercise.Filters
{
    public class ValidationExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException validationEx)
            {
                var validation = new ValidationProblemDetails(new Dictionary<string, string[]>(validationEx?.ValidationResult?.MemberNames?.Select(s => new KeyValuePair<string, string[]>(s, new[] { validationEx.ValidationResult.ErrorMessage }))));

                context.Result = new BadRequestObjectResult(validation);

                context.ExceptionHandled = true;
            }
        }
    }
}
