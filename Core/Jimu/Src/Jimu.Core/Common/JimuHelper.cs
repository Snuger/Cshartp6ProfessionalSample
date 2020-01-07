using System;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace Jimu.Core.Common
{

    public static class JimuHelper
    {

        public static IConfigurationRoot GetConfig(string settingJson)
        {
            JsonEnvParamParserFileProvider provider = new JsonEnvParamParserFileProvider(settingJson);
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile(provider, settingJson, true, false);
            return builder.Build();
        }

        public static IConfigurationRoot ReadSettings(string settingFileName)
        {
            string jimuAppSettings = $"{settingFileName}.json";
            string env = ReadJimuEnv();
            if (!string.IsNullOrEmpty(env))
                jimuAppSettings = $"{settingFileName}.{env}.json";
            if (!File.Exists(jimuAppSettings))
            {
                throw new FileNotFoundException($"{jimuAppSettings} not found");
            }
            return GetConfig(jimuAppSettings);
        }

        public static string ReadJimuEnv()
        {
            string jimuSetting = "JimuSettings.json";
            string jimuEnv = "JIMU_ENV";
            string activeProfile = "";

            if (File.Exists(jimuSetting))
            {
                var config = GetConfig(jimuSetting);
                if (config != null && config["ActiveProfile"] != null)
                    activeProfile = config["ActiveProfile"];

            }

            if (string.IsNullOrEmpty(activeProfile))
            {
                var builder = new ConfigurationBuilder();
                var config = builder.AddEnvironmentVariables().Build();
                if (config != null && config[jimuEnv] != null)
                    activeProfile = config[jimuEnv];
            }
            return activeProfile?.Trim();

        }
    }
}