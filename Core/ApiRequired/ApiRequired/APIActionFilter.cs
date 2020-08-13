using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiRequired
{
    public class APIActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                StringBuilder error = new StringBuilder();
                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        error.Append($"{key}:{state.Errors.First().ErrorMessage};");
                    }
                }

                // PublicResponse result = new PublicResponse()
                // {
                //     Status = "400",
                //     Errors = error.ToString().Trim(';')
                // };
                // context.Result = new JsonResult(result);
                context.Result = new ContentResult()
                {
                    Content = error.ToString().Trim(';'),
                    StatusCode = 400
                };
            }
        }
    }
}