using Microsoft.Extensions.DependencyInjection;
using PetstoreTest.Api.Services.PetstoreService;
using TestFramework.Config;

namespace PetstoreTest
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ConfigReader.ReadConfig());
            services.AddScoped<IPetstoreApiService, PetstoreApiService>();
        }
    }
}
