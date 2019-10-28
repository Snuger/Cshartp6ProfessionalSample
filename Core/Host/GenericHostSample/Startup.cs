using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GenericHostSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection service) {
            
            service.AddOptions();
            service.Configure<MyOptions>(opt =>
            {
                opt.Optinos1 = "你是我一生最爱的人";
                opt.Options2 = 99999;
            });
        }
    }
}
