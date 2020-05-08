using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Sampke.Core
{
    public class Application : IApplication
    {

        private readonly List<Action<IContainer>> _disposeActions;

        private readonly List<Action<IContainer>> _runActions;

        private readonly List<Action<IContainer>> _beforeRunActions;


        public IContainer Container { get; set; }

        public IConfigurationRoot AppSettings { get; set; }

        public Application(List<Action<IContainer>> beforeRunActions = null, List<Action<IContainer>> runActions = null, List<Action<IContainer>> disposeActions = null)
        {
            _disposeActions = disposeActions ?? new List<Action<IContainer>>();
            _runActions = runActions ?? new List<Action<IContainer>>();
            _beforeRunActions = beforeRunActions ?? new List<Action<IContainer>>();
        }

        public IApplication BeforeRunAction(Action<IContainer> action)
        {
            _beforeRunActions.Add(action);
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
            if (_beforeRunActions.Any())
                _beforeRunActions.ForEach(x => { x(Container); });
            if (_runActions.Any())
                _runActions.ForEach(x => { x(Container); });
        }

        public IApplication RunAction(Action<IContainer> action)
        {
            _runActions.Add(action);
            return this;
        }
    }
}