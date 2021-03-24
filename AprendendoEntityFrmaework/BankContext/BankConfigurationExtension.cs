using AprendendoEntityFrmaework.BankContext.ModelsConfiguring;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.BankContext
{
    public static class BankConfigurationExtension
    {
        public static void MultipleApplyConfiguration(this ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbank");
            builder.ApplyConfiguration(new BankConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new BankClientConfiguration());
        }
    }
}
