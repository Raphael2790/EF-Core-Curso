using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.MusicContext
{
    public static class OptionsBuilderExtension
    {
        public static void LogConfiguration(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .LogTo(Console.WriteLine,
                    (eventId, loglevel) => loglevel == LogLevel.Information
                    && eventId == RelationalEventId.CommandExecuted,
                    DbContextLoggerOptions.Category)
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name });
        }
    }
}
