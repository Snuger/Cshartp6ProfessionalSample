using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcApplicationSample.Controllers
{
    public class POCOController 
    {
        public string index() => "this is POCO controller";

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public HttpContext Context => ActionContext.HttpContext;

        public ModelStateDictionary ModelState => ActionContext.ModelState;

        public string UserAgentInfo() {
            if (Context.Request.Headers.ContainsKey("User-Agent")) {
                return Context.Request.Headers["User-Agent"];
            }
            return "No User-Agent infomation";
        }
    }
}