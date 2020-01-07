using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore3Example.Services;


namespace AspNetCore3Example.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrders(int id)
        {
            return await Task.Run(() =>
            {
                return Ok(orderService.GetOrderById(id));
            });
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return await Task.Run(() =>
            {
                return Ok(orderService.GetAllOrders());
            });
        }

    }
}