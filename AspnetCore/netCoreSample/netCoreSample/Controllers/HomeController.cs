using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netCoreSample.Services;

namespace netCoreSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISampleServices _services;

        public HomeController(ISampleServices services)
        {
            _services = services;
        }

        public async Task<int>Index(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            sb.Append(string.Join("",_services.GetSampleString().Select(
                s=>$"<li>{s}</li>").ToArray()
                ));
            sb.Append("</ul>");
            await context.Response.WriteAsync(sb.ToString());
            return 200;
        }
    }
}