using System;
using Autofac;
using System.Collections.Generic;

namespace Sampke.Core
{
    public abstract class ApplicationBuilderBase<T> : IApplicationBuilder<T> where T : class
    {
        protected List<Action<ContainerBuilder>> ModuleRegisters { get; }

        protected List<Action<ContainerBuilder>> BeforeBuilders { get; }

        protected List<Action<IContainer>> Initializers { get; }

        protected List<Action<IContainer>> BeforeRunners { get; }

        protected List<Action<IContainer>> Runners { get; }

        protected ContainerBuilder ContainerBuilder { get; }

        protected string SettingName { get; }

        public ApplicationBuilderBase(ContainerBuilder containerBuilder, string settingName)
        {
            ContainerBuilder = containerBuilder;
            ModuleRegisters = new List<Action<ContainerBuilder>>();
            BeforeBuilders = new List<Action<ContainerBuilder>>();
            Initializers = new List<Action<IContainer>>();
            BeforeRunners = new List<Action<IContainer>>();
            Runners = new List<Action<IContainer>>();
            SettingName = settingName;
        }

        public virtual T AddBeforeBuilder(Action<ContainerBuilder> beforeBuilder)
        {
            BeforeBuilders.Add(beforeBuilder);
            return this as T;
        }

        public virtual T AddBeforeRuner(Action<IContainer> beforRunner)
        {
            BeforeRunners.Add(beforRunner);
            return this as T;
        }

        public virtual T AddInitializer(Action<IContainer> initializer)
        {
            Initializers.Add(initializer);
            return this as T;
        }

        public virtual T AddRegister(Action<ContainerBuilder> moduleRegister)
        {
            ModuleRegisters.Add(moduleRegister);
            return this as T;
        }

        public virtual T AddRuner(Action<IContainer> runner)
        {
            Runners.Add(runner);
            return this as T;
        }

        public virtual IApplication Build()
        {
            LoadModule();

            IContainer container = null;
            var host = new Application(BeforeRunners, Runners, null);
            ContainerBuilder.Register(x => host).As<IApplication>().SingleInstance();
            ContainerBuilder.Register(x => container).As<IContainer>().SingleInstance();
            //ContainerBuilder.RegisterType<ConsoleLogger>

            ModuleRegisters.ForEach(x => { x(ContainerBuilder); });
            BeforeBuilders.ForEach(x => { x(ContainerBuilder); });

            container = ContainerBuilder.Build();

            Initializers.ForEach(x => { x(container); });

            host.Container = container;
            //host.AppSettings = SettingName;



            return host;
        }



        protected abstract void LoadModule();
    }
}