using AprendendoEntityFramework.MusicContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.MusicContext.ModelsConfiguring
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Album");

            builder.HasKey(x => x.AlbumId);

            builder.Property(x => x.AlbumId).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.HasIndex(x => x.AlbumId);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Ano)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Link)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(x => x.Musics)
                .WithOne(x => x.Album)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
               new List<Album>()
               {
                    new Album 
                    { 
                      AlbumId = 1,
                      Nome = "Carnaval 2018", 
                      Ano=2018, 
                      Link="https://www.escolasdesamba.com.br"
                    }
               }
           );
        }
    }
}
