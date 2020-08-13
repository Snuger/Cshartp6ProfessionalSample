using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sampke.Basic.Module
{

    public enum ModuleExecPriority
    {

        /// <summary>
        ///  execute first
        /// </summary>
        Critical = 0,
        /// <summary>
        /// after Critical
        /// </summary>
        Important = 1,
        /// <summary>
        /// after Important
        /// </summary>
        Normal = 2,
        /// <summary>
        ///  last
        /// </summary>
        Low = 3

    }


    public  abstract class ModuleBase
    {
        protected IConfigurationRoot configSettings { get; }

        public virtual ModuleExecPriority Priority => ModuleExecPriority.Important;

        protected ModuleBase(IConfigurationRoot configSettings)
        {
            this.configSettings = configSettings;
        }

        public virtual void DoRegistr(ContainerBuilder componentContainerBuilder)
        {

        }


        public virtual void Doint(IContainer container)
        {

        }


        public virtual void DoRun(IContainer container)
        {

        }


        public virtual void DoDispose(IContainer container)
        {

        }

         
    }
}
