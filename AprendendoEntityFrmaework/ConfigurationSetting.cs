using Castle.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework
{
    public class ConfigurationSetting
    {
        private static IConfiguration _configuration;
        public ConfigurationSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
