using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;


namespace AspnetCoreExample.Services
{
    public class ValuesServices : IValuesService
    {
        private readonly ILogger<ValuesServices> logger;

        private static readonly string[] Summaries = new[]
           {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public ValuesServices(ILogger<ValuesServices> logger)
        {
            this.logger = logger;
        }

        public async Task<string> GetValueById(int id)
        {
            return await Task.Run(() =>
            {
                if (id < 0 || id > 7)
                    return "数据超出界限";
                return Summaries[id];
            });
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            return await Task.Run(() =>
            {
                return Summaries;
            });
        }
    }
}