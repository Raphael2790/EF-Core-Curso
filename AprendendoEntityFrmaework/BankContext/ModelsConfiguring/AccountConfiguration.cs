using AprendendoEntityFrmaework.BankContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.ModelsConfiguring
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts")
                    .HasKey(x => x.AccountID);

            builder.Property(x => x.Created_at)
                .HasColumnName("Created_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AccountNumber)
                .HasColumnName("AccountNumber")
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            //Ao determinamos uma propriedade como concurrency token o entity framework
            //Vai se certificar que não houve mudança nessa proriedade da tabela é a mesma desde que 
            //ele trouxe o valor para a memória até o momento de inserir 
            //Caso o valor seja diferente ele lançara uma exception do tipo DbUpdateConcurrency
            builder.Property(x => x.AccountBalance)
                .HasColumnType("decimal")
                .HasColumnName("AccountBalance")
                .HasDefaultValue(0.00M)
                .IsConcurrencyToken()
                .IsRequired()
                .HasPrecision(15, 2)
                .HasField("_accountBalance");//Estamos informando que o valor que será salvo está em uma propriedade privada

            builder.Property(x => x.AccountType)
                .IsRequired()
                .HasColumnName("AccountType")
                .HasConversion<string>()
                .HasColumnType("varchar(20)");

            builder.HasIndex(x => x.AccountID);

            builder.HasOne(x => x.BankAgency)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.BankAgencyId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            //Quando determinamos a chave estrangeira ela pode ter um relacionamento opcional basta não uar o required
            //DeleteBehavior cria uma notação para quando a entidade principal for deletada o que acontecerá com as dependentes
            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Account)
                .HasForeignKey(x => x.ClientID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();//Opcional caso já tenha declarado a HasForeignKey

        }
    }
}
