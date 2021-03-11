using AprendendoEntityFrmaework.BankContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.ModelsConfiguring
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Banks")
                .HasKey(x => x.BankID);

            builder.Property(x => x.BankCode)
                .IsRequired()
                .HasColumnName("BankCode")
                .HasColumnType("varchar(50)");

            builder.Property(x => x.BankName)
                .IsRequired()
                .HasColumnName("BankCode")
                .HasColumnType("varchar(50)");

            builder.HasIndex(x => x.BankID);

            builder.HasMany(x => x.Agencies).WithOne(x => x.Bank);
        }
    }
}
