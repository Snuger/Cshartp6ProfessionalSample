using System;
using Autofac;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Jimu.Core.Common;

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


        protected ApplicationBuilderBase(ContainerBuilder containerBuilder, string settingName)
        {
            this.ContainerBuilder = containerBuilder;
            this.JimuAppSettings = JimuHelper.ReadSettings(settingName);
            BeforeRunner = new List<Action<IContainer>>();
            Runners = new List<Action<IContainer>>();
            Initializer = new List<Action<IContainer>>();
            ModuleRegisters = new List<Action<ContainerBuilder>>();
        }

        public virtual T AddBeforeRunner(Action<IContainer> beforeRunner)
        {
            BeforeRunner.Add(beforeRunner);
            return this as T;
        }

        public virtual T AddInitializer(Action<IContainer> initializer)
        {
            Initializer.Add(initializer);
            return this as T;
        }

        public virtual T AddModel(Action<ContainerBuilder> modelBuilder)
        {
            ModuleRegisters.Add(modelBuilder);
            return this as T;
        }

        public virtual T AddRunner(Action<IContainer> runner)
        {
            Runners.Add(runner);
            return this as T;
        }

        public virtual IApplication Build()
        {
            IContainer container = null;
            var host = new Application(BeforeRunner, Runners, null);
            ContainerBuilder.Register(x => host).As<IApplication>().SingleInstance();
            ContainerBuilder.Register(x => container).As<IContainer>().SingleInstance();

            ModuleRegisters.ForEach(x => { x(ContainerBuilder); });
            container = ContainerBuilder.Build();

            Initializer.ForEach(x => { x(container); });

            host.Container = container;
            host.JimuAppSettings = JimuAppSettings;

            return host;
        }
    }
}