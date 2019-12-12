
using System;
using Autofac;

namespace Jimu.Core
{
    public interface IApplicationBuilder<out T> where T : class
    {

        T AddModel(Action<ContainerBuilder> Builder);


        T AddInitializer(Action<IContainer> initializer);

        T AddBeforeRunner(Action<IContainer> beforeRunner);

        T AddRunner(Action<IContainer> runner);


        IApplication Builder();

    }
}