using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthorizationSamples.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuthorizationSamples.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
               var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
               var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        
              // 想要获得 refreshToken 必须在MVC客户端的 Scope 单独添加 OfflineAccess
               var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
    
                ViewData["accessToken"] = accessToken;
                ViewData["idToken"] = idToken;
                ViewData["refreshToken"] = refreshToken;
    
               return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
