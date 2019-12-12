using System;
using Autofac;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Jimu.Core
{
    public class ApplicationBuilderBase<T> : IApplicationBuilder<T> where T : class
    {

        protected List<Action<IContainer>> BeforeRunner { get; }

        protected List<Action<IContainer>> Initializer { get; }

        protected List<Action<IContainer>> Runners { get; }

        protected List<Action<ContainerBuilder>> ModuleRegisters { get; }


        protected ContainerBuilder ContainerBuilder { get; }

        protected IConfigurationRoot JimuAppSettings { get; }


        public ApplicationBuilderBase(ContainerBuilder containerBuilder, string settingName)
        {
            this.ContainerBuilder = containerBuilder;
            // this.JimuAppSettings = jimuAppSettings;
            BeforeRunner = new List<Action<IContainer>>();
            Runners = new List<Action<IContainer>>();
            Initializer = new List<Action<IContainer>>();
            ModuleRegisters = new List<Action<ContainerBuilder>>();
        }

        public T AddBeforeRunner(Action<IContainer> beforeRunner)
        {
            BeforeRunner.Add(beforeRunner);
            return this as T;
        }

        public T AddInitializer(Action<IContainer> initializer)
        {
            Initializer.Add(initializer);
            return this as T;
        }

        public T AddModel(Action<ContainerBuilder> modelBuilder)
        {
            ModuleRegisters.Add(modelBuilder);
            return this as T;
        }

        public T AddRunner(Action<IContainer> runner)
        {
            Runners.Add(runner);
            return this as T;
        }

        public IApplication Builder()
        {
            throw new NotImplementedException();
        }
    }
}