using Autofac;
using Microsoft.Extensions.Configuration;
using Sampke.Basic.Module;
using Sampke.Server.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sampke.Server
{
    public class JwtAuthServerModule : ServerModuleBase
    {

        private readonly JwtAuthorizationOptions _options;

        public JwtAuthServerModule(IConfigurationRoot configSettings) : base(configSettings)
        {
            _options = configSettings.GetSection(typeof(JwtAuthorizationOptions).Name).Get<JwtAuthorizationOptions>();
        }

        public override ModuleExecPriority Priority => ModuleExecPriority.Critical;

        public override void Doint(IContainer container)
        {

            base.Doint(container);
        }
    }
}
