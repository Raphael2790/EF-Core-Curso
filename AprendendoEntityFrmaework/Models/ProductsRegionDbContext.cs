using System;
using AprendendoEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#nullable disable

namespace AprendendoEntityFrmaework.Models
{
    //Contexto gerado via scaffold das tabelas no banco 
    public partial class ProductsRegionDbContext : DbContext
    {
        public ProductsRegionDbContext()
        {
        }

        public ProductsRegionDbContext(DbContextOptions<ProductsRegionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseSqlServer("Data Source=DESKTOP-083D5DN\\SQLEXPRESS;Initial Catalog=CursoEFCoreBD;Integrated Security=True;Pooling=False");
            }

            optionsBuilder
                .LogTo(Console.WriteLine,(eventId,level) => level == LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MultipleConfigurationProducts();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
