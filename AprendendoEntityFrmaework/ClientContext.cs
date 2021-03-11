using AprendendoEntityFrmaework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework
{
    //Contexto gerado antecipado e usado migration com data annotation
    public class ClientContext : DbContext
    {
        public ClientContext()
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-083D5DN\\SQLEXPRESS;Initial Catalog=CursoEFCoreBD;Integrated Security=True;Pooling=False");
            }

            optionsBuilder
                .LogTo(Console.WriteLine, (category, level) => level == LogLevel.Information);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity => {
                entity.Property(x => x.RegisterDate)
                                .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
