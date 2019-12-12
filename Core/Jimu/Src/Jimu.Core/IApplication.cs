using System;
using Autofac;

namespace Jimu.Core
{
    public interface IApplication : IDisposable
    {
        IContainer Container { get; }

        IApplication DisposeAction(Action<IContainer> action);

        IApplication RunAction(Action<IContainer> action);

        IApplication BeforeRunAction(Action<IContainer> action);

        void Run();
    }
}