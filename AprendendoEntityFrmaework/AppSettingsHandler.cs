using AprendendoEntityFramework.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AprendendoEntityFramework
{
    class AppSettingsHandler
    {
        private IFileProvider _fileProvider;
        private const string FILE_NAME = "appsettings.json";
        private const string BASE_FILE_PATH = "C:\\Users\\Raphael Silvestre\\source\\repos\\AprendendoEntityFrmaework\\AprendendoEntityFrmaework";
        private AppSettingsModel _config;
        public AppSettingsHandler(IFileProvider fileProvider)
        {
            _config = GetAppSettings();
            _fileProvider = fileProvider;
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
