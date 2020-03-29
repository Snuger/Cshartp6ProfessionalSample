using System;
using Autofac;
using AspnetCoreExample.Services;
using Microsoft.Extensions.Logging;

namespace AspnetCoreExample
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ValuesServices(c.Resolve<ILogger<ValuesServices>>())).As<IValuesService>().InstancePerLifetimeScope();

        }

    }
}