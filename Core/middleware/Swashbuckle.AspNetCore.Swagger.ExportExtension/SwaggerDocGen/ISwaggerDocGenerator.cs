using System.IO;
using System.Threading.Tasks;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.SwaggerDocGen
{
    public interface ISwaggerDocGenerator
    {
        
        /// <summary>
         /// 获取Swagger流文件
         /// </summary>
         /// <param name="name"></param>
         /// <returns></returns>
        Task<MemoryStream> GetSwaggerDocStreamAsync(string name);


        /// <summary>
        /// 获取Swagger MarkDown源代码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetSwaggerDoc(string name);
    }
}