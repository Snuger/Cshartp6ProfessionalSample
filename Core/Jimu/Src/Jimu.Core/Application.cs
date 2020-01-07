using System;
using System.Linq;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Jimu.Core
{
    public class Application : IApplication
    {
        private readonly List<Action<IContainer>> _disposeActions;
        private readonly List<Action<IContainer>> _beforeRunenrActions;
        private readonly List<Action<IContainer>> _runnerActions;

        public Application(List<Action<IContainer>> beforeRunenrActions = null, List<Action<IContainer>> runnerActions = null, List<Action<IContainer>> disposeActions = null)
        {
            _disposeActions = _disposeActions ?? new List<Action<IContainer>>();
            _beforeRunenrActions = beforeRunenrActions ?? new List<Action<IContainer>>();
            _runnerActions = runnerActions ?? new List<Action<IContainer>>();
        }

        public IContainer Container { get; set; }

        public IConfigurationRoot JimuAppSettings { get; set; }


        public IApplication BeforeRunAction(Action<IContainer> action)
        {
            _beforeRunenrActions.Add(action);
            return this;
        }

        public void Dispose()
        {
            if (_disposeActions.Any())
                _disposeActions.ForEach(x => { x(Container); });
            Container.Dispose();
        }

        public IApplication DisposeAction(Action<IContainer> action)
        {
            _disposeActions.Add(action);
            return this;
        }

        public void Run()
        {
            if (_beforeRunenrActions.Any())
                _beforeRunenrActions.ForEach(x => { x(Container); });
            if (_runnerActions.Any())
                _runnerActions.ForEach(x => { x(Container); });
        }

        public IApplication RunAction(Action<IContainer> action)
        {
            _runnerActions.Add(action);
            return this;
        }
    }
}