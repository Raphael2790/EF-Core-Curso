using AprendendoEntityFrmaework.BankContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.ModelsConfiguring
{
    public class BankClientConfiguration : IEntityTypeConfiguration<BanksClients>
    {
        //Configuração da tabela de asssociação em um relacionamento muito para muitos
        public void Configure(EntityTypeBuilder<BanksClients> builder)
        {
            builder.ToTable("BankClient");

            builder.HasKey(x => new { x.BankId, x.ClientId });

            builder.HasOne(x => x.Bank)
                    .WithMany(x => x.BankClients).HasForeignKey(x => x.BankId);

            builder.HasOne(x => x.Client)
                    .WithMany(x => x.BankClients).HasForeignKey(x => x.ClientId);

            builder.Property(x => x.AssociatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
