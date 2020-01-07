using System;
using Autofac;
using System.Linq;
using System.Collections.Generic;
using Jimu.Core;

namespace Jimu.Server
{
    public class ApplicationServerBuilder : ApplicationBuilderBase<ApplicationServerBuilder>
    {
        public List<Action<ContainerBuilder>> ServiceRegisters = new List<Action<ContainerBuilder>>();
        public List<Action<IContainer>> ServiceInitializers = new List<Action<IContainer>>();

        public ApplicationServerBuilder(ContainerBuilder container, string settingsName = "JimuAppServerSettings") : base(container, settingsName)
        {
        }

        public override ApplicationServerBuilder AddRunner(Action<IContainer> runner) => base.AddRunner(runner);
        public override ApplicationServerBuilder AddBeforeRunner(Action<IContainer> beforeRunner) => base.AddBeforeRunner(beforeRunner);
        public override ApplicationServerBuilder AddInitializer(Action<IContainer> initializer) => base.AddInitializer(initializer);

        public override ApplicationServerBuilder AddModel(Action<ContainerBuilder> modelBuilder) => base.AddModel(modelBuilder);


        public virtual ApplicationServerBuilder AddServiceModule(Action<ContainerBuilder> modelBuilder)
        {
            ServiceRegisters.Add(modelBuilder);
            return this;
        }

        public virtual ApplicationServerBuilder AddServiceRegisters(Action<IContainer> initializer)
        {
            ServiceInitializers.Add(initializer);
            return this;
        }


        public override IApplication Build()
        {

            return base.Build();
        }
    }
}