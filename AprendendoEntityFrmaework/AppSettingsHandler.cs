using AprendendoEntityFramework.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AprendendoEntityFramework
{
    class AppSettingsHandler
    {
        private const string FILE_NAME = "appsettings.json";
        private const string BASE_FILE_PATH = "C:\\Users\\Raphael Silvestre\\source\\repos\\AprendendoEntityFrmaework\\AprendendoEntityFrmaework";
        private AppSettingsModel _config;

        public AppSettingsHandler()
        {
            _config = GetAppSettings();
        }

        public AppSettingsModel GetAppSettings()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(BASE_FILE_PATH)
               .AddJsonFile(FILE_NAME, false, true)
               .Build();

            return config.GetSection("App").Get<AppSettingsModel>();
        }
    }
}
