using hostsampke.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace hostsampke.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _iConfiguration;

        public AccountController(IJWTService jwtService, IConfiguration iConfiguration)
        {
            _jwtService = jwtService;
            _iConfiguration = iConfiguration;
        }

        [HttpGet("login")]
        public IActionResult Login(string name, string passwd)
        {
            ///这里应该是需要去连接数据库做数据校验，为了方便所有用户名和密码写死了
            if ("admin".Equals(name) && "123456".Equals(passwd))//应该数据库
            {
                string token = _jwtService.GetToken(name);
                return new JsonResult(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return new JsonResult(new
                {
                    result = false,
                    token = string.Empty
                });
            }

        }

    }
}