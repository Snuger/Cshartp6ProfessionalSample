using System;
using System.IO;
using System.Threading.Tasks;

namespace MCRP.External.Jmeter.Utilities
{
   public interface IJmxFileOperator
    {
            Task<bool> SaveUploadFile(string id, string boundary, Stream stream);

    }

}