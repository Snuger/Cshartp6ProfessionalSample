using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sampke.Basic.Module
{
    public class ServerModuleBase : ModuleBase
    {
        public ServerModuleBase(IConfigurationRoot configSettings) : base(configSettings)
        {
        }

        public virtual void  DoServiceRegister(ContainerBuilder builder)
        {

        }


        public virtual void DoServiceInit(IContainer container)
        {

        }


    }
}
