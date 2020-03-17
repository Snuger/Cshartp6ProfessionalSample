using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationSamples.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class CustomerController: ControllerBase
    {

      [HttpPost]
       public IActionResult Post(Customer customer){           
            if(ModelState.IsValid) {               
                //处理操作
		    }
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(){
            Customer customer = new Customer()
            {
                UserName = "",  
                Age = 20,
                Id=1
             };
            return Ok(customer);
        }
        
    }
}