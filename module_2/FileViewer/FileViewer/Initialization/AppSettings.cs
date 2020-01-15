using Microsoft.Extensions.Configuration;
using System.IO;

namespace FileViewer.Initialization
{
    public class AppSettings
    {
        public IConfigurationRoot InitializationConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return config;
        }
    }
}
