using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Sampke.Core.Common
{
    public static class AppConfigSettings
    {

        /// <summary>
        /// get config from specify file which locate in app root
        /// </summary>
        /// <param name="fileName">setting json file</param>
        /// <returns></returns>
        public static IConfigurationRoot GetConfig(string settingJson)
        {
            // var provider = new JsonEnvParamParserFileProvider(settingJson);
            // var builder = new ConfigurationBuilder()
            //     .SetBasePath(AppContext.BaseDirectory).AddJsonFile(provider, settingJson, true, false);
            // return builder.Build();
            return null;
        }


        public static IConfigurationRoot ReadSetting(string settingFileName)
        {
            var appSettings = $"{settingFileName}.json";
            var env = ReadJimuEvn();
            if (!string.IsNullOrEmpty(env))
            {
                appSettings = $"{settingFileName}.{env}.json";
            }
            if (!File.Exists(appSettings))
            {
                throw new FileNotFoundException($"{appSettings} not found!");
            }
            return GetConfig(appSettings);
        }

        public static string ReadJimuEvn()
        {
            var jimuSettings = "JimuSettings.json";
            var jimuEnv = "ASPNETCORE_ENV";
            string activeProfile = "";

            // if (File.Exists(jimuSettings))
            // {
            //     var config = JimuHelper.GetConfig(jimuSettings);
            //     if (config != null && config["ActiveProfile"] != null)
            //     {
            //         activeProfile = config["ActiveProfile"];
            //     }
            // }
            // if (string.IsNullOrEmpty(activeProfile?.Trim()))
            // {
            //     var builder = new ConfigurationBuilder();
            //     var config = builder.AddEnvironmentVariables().Build();
            //     if (config != null && config[jimuEnv] != null)
            //     {
            //         activeProfile = config[jimuEnv];
            //     }
            // }
            return activeProfile?.Trim();
        }
    }
}