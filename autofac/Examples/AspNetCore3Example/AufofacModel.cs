using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AspNetCore3Example.Services;
using Microsoft.Extensions.Logging;

namespace AspNetCore3Example
{
    public class AutofacModel : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new OrderService(c.Resolve<ILogger<OrderService>>())).As<IOrderService>().InstancePerLifetimeScope();
        }
    }

}