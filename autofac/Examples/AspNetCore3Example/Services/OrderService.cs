using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AspNetCore3Example.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> logger;

        private static readonly string[] Summaries = new[]
     {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public OrderService(ILogger<OrderService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<string> GetAllOrders()
        {
            logger.LogDebug($"{nameof(this.GetAllOrders)} 方法被调用");
            return Summaries;
        }

        public string GetOrderById(int id)
        {
            logger.LogDebug($"{nameof(this.GetOrderById)} 方法被调用");
            if (id < 0 || id > 8)
                return "索引超出界限";
            return Summaries[id];
        }
    }
}