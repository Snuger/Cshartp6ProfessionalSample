using System;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Sampke.Core
{
    public interface IApplication : IDisposable
    {
        IContainer Container { get; }

        IConfigurationRoot AppSettings { get; }

        IApplication DisposeAction(Action<IContainer> action);

        IApplication RunAction(Action<IContainer> action);

        IApplication BeforeRunAction(Action<IContainer> action);

        void Run();

    }
}