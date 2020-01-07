using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;


namespace Jimu.Core.Common
{
    public class JsonEnvParamParserFileProvider : IFileProvider
    {

        private class InMemoryFile : IFileInfo
        {
            protected byte[] _data { get; }
            public InMemoryFile(string json)
            {
                string fileStr = string.Empty;
                using (StreamReader reader = new StreamReader(json))
                {
                    fileStr = reader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(fileStr))
                {
                    var builder = new ConfigurationBuilder();
                    var config = builder.AddEnvironmentVariables().Build();
                    Regex regex = new Regex("([$]{[\\w\\d-_.]+})");
                    var matches = regex.Matches(fileStr);
                    foreach (Match match in matches)
                    {
                        var envName = match.Value.TrimStart(new char[] { '{', '$' }).TrimEnd('}');
                        var envValue = config[envName];
                        if (!string.IsNullOrEmpty(envValue))
                            fileStr = fileStr.Replace(match.Value, envValue.Replace(@"\", @"\\"));
                    }
                }
                _data = Encoding.UTF8.GetBytes(fileStr);
            }

            public bool Exists { get; } = true;

            public long Length => _data.Length;

            public string PhysicalPath { get; } = string.Empty;

            public string Name { get; } = string.Empty;

            public DateTimeOffset LastModified { get; } = DateTimeOffset.UtcNow;
            public bool IsDirectory { get; } = false;
            public Stream CreateReadStream() => new MemoryStream(_data);
        }

        public JsonEnvParamParserFileProvider(string json) => _fileInfo = new InMemoryFile(json);

        private readonly IFileInfo _fileInfo;
        public IDirectoryContents GetDirectoryContents(string _) => null;
        public IFileInfo GetFileInfo(string _) => _fileInfo;
        public IChangeToken Watch(string _) => NullChangeToken.Singleton;
    }
}