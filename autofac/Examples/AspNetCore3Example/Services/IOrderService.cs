using System;
using System.Collections.Generic;

namespace AspNetCore3Example.Services
{
    public interface IOrderService
    {
        string GetOrderById(int id);
        IEnumerable<string> GetAllOrders();

    }
}