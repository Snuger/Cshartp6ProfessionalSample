using Autofac;

namespace Jimu.Core
{
    public class ApplicationBuilder : ApplicationBuilderBase<ApplicationBuilder>
    {
        public ApplicationBuilder(ContainerBuilder containerBuilder, string settingName = "JimuAppServerSettings") : base(containerBuilder, settingName)
        {

        }

    }
}