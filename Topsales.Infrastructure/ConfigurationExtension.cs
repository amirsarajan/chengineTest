using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topsales.Infrastructure
{
    public  static class ConfigurationExtension
    {
        public static string GetApiKey(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("ApiKey");
        }
    }
}
