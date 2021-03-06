using AprendendoEntityFrmaework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.Models
{
    public static class ProductConfigurationExtension
    {
        public static void MultipleConfigurationProducts(this ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Ignore<Category>();

            //Podemos mapear duas entidades para a mesma tabela utilizando o ToTable com nomes iguais
            //E é somente possível quando existe um relacionamento 1 para 1 e ambas devem possuir chave estrangeira apontando para a primaria da outra ambas com mesmo nome
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                //Toda consulta na entidade de produto receberá esse filtro automaticamente
                //entity.HasQueryFilter(x => x.CategoryID != 0);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryID)
                    .HasColumnType("int");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                //Ao definir uma coluna na tabela do tipo byte[] para controle de versão
                //O entity irá aplicar a concorrencia otimista porém ao invés de comparar um propriedade com token
                //Criamos um campo especifico para tratar a concorrencia na tabela
                //Nem todos provedores de banco de dados suportam essa abordagem
                //entity.Property(x => x.RowVersion).IsRowVersion();

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);

            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionDescription)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasColumnType("nvarchar(50)");

                entity.HasKey(e => e.CategoryID);

                entity.Property(e => e.CategoryID).HasColumnType("int");

                entity.HasMany(x => x.Products).WithOne(x => x.Category).OnDelete(DeleteBehavior.SetNull);

            });
        }
    }
}
