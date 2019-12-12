using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Jimu.Core.Common
{
    public class JsonEnvParamParserFileProvider : IFileProvider
    {

        public class InMemoryFile : IFileInfo
        {
            public bool Exists => throw new NotImplementedException();

            public long Length => throw new NotImplementedException();

            public string PhysicalPath => throw new NotImplementedException();

            public string Name => throw new NotImplementedException();

            public DateTimeOffset LastModified => throw new NotImplementedException();

            public bool IsDirectory => throw new NotImplementedException();

            public Stream CreateReadStream()
            {
                throw new NotImplementedException();
            }
        }


        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}