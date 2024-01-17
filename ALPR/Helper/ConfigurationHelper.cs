using ALPR.Models;
using Microsoft.Extensions.Configuration;

namespace ALPR.Helper
{
    public static class ConfigurationHelper
    {
        public static T GetConfiguration<T>(string section) where T : new()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                                .SetBasePath(currentDirectory)
                                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            var configurations = config.GetSection(section).Get<T>();
            return configurations;
        }
    }
}
