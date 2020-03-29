using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Extensions
{
    public class LanguageAttribute:ActionFilterAttribute
    {
        private string _language = null;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _language = context.RouteData.Values["language"] == null ?
                null : context.RouteData.Values["language"].ToString();

            //base.OnActionExecuting(context);
        }
    }
}
