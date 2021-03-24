using AprendendoEntityFramework.BankContext;
using AprendendoEntityFrmaework.BankContext.Models;
using AprendendoEntityFrmaework.BankContext.ModelsConfiguring;
using AprendendoEntityFrmaework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext
{
    public class BankDbContext : DbContext
    {
        public BankDbContext()
        {

        }

        //o contrutor com a options nos permitirá trocar o banco de dados e viabilizar testes de unidade
        public BankDbContext(DbContextOptions options)
        {
            
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BankAgency> BankAgencies { get; set; }
        public DbSet<BanksClients> BankClients { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-083D5DN\\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True;Pooling=False")
                .LogTo(Console.WriteLine, ( _ , level) => level == LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ignora uma classe e não mapea a tabela
            //modelBuilder.Ignore<Bank>();

            //Criando uma extensão de modelbuilder para aplicar as configurações multiplas
            modelBuilder.MultipleApplyConfiguration();

            //Configuração para pegar todas classes que herdam de EntityType
            //modelBuilder.ApplyConfigurationsFromAssembly(
            //    Assembly.GetExecutingAssembly(),
            //    t => t.GetInterfaces().Any(i =>
            //    i.IsGenericType &&
            //    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        }

        [DbFunction(Schema = "Admin", Name = "ClientsWithNegativeBalance")]
        public static decimal ClientsWithNegativeBalance(int clientId)
        {
            return 0;
        }
    }
}
