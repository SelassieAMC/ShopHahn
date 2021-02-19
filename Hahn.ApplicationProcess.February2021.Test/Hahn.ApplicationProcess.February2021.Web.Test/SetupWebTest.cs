using Hahn.ApplicationProcess.February2021.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Hahn.ApplicationProcess.February2021.Test.Hahn.ApplicationProcess.February2021.Web.Test
{
    public class SetupWebTest
    {
        private readonly IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: false)
            .Build();
        public IServiceCollection GetServices()
        {
            var startup = new Startup(config);
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);
            return serviceCollection;
        }
    }
}
