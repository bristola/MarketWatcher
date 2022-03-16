using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utilities.services.contracts;

namespace Utilities.services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string _settingsFileName = "appsettings.json";

        public string GetString(string section)
        {
            var config = GetConfiguration();

            return config.GetSection(section)?.Value;
        }

        public string GetConnectionString(string connectionName)
        {
            var config = GetConfiguration();

            return config.GetConnectionString(connectionName);
        }
        
        public bool GetBool(string section)
        {
            var config = GetConfiguration();

            return Convert.ToBoolean(config.GetSection(section)?.Value);
        }

        public int GetInt(string section)
        {
            var config = GetConfiguration();

            return Convert.ToInt32(config.GetSection(section)?.Value);
        }

        public double GetDouble(string section)
        {
            var config = GetConfiguration();

            return Convert.ToDouble(config.GetSection(section)?.Value);
        }

        private IConfiguration GetConfiguration() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_settingsFileName, false, true)
            .Build();
    }
}
