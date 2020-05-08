using Autofac;
using System;
using Microsoft.Extensions.Configuration;

namespace Sampke.Core
{
    public interface IApplicationBuilder<out T> where T : class
    {
        T AddRegister(Action<ContainerBuilder> moduleRegister);

        T AddBeforeBuilder(Action<ContainerBuilder> beforeBuilder);

        IApplication Build();

        T AddInitializer(Action<IContainer> initializer);

        T AddBeforeRuner(Action<IContainer> beforRunner);

        T AddRuner(Action<IContainer> runner);

    }
}