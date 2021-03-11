using AprendendoEntityFramework.MusicContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.MusicContext.ModelsConfiguring
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.ToTable("Music");

            builder.HasKey(x => x.MusicId);

            builder.Property(x => x.MusicId)
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.Album)
                .WithMany(x => x.Musics)
                .HasForeignKey(x => x.AlbumId)
                .IsRequired();

            builder.HasIndex(x => x.MusicId);

            builder.HasData(
                new List<Music>()
                      {
                          new Music {MusicId=1, Name = "Enredo Escola de Samba Beija-Flor", AlbumId=1},
                          new Music {MusicId=2, Name = "Enredo Escola de Samba Academicos Tucuruvi", AlbumId=1},
                          new Music {MusicId=3, Name = "Enredo Escola de Samba Vai-vai", AlbumId=1},
                          new Music {MusicId=4, Name = "Enredo Escola de Samba Gaviões da Fiel", AlbumId=1},
                          new Music {MusicId=5, Name = "Enredo Escola de Samba Dragões da Real", AlbumId=1},
                      }
            );
        }   
    }
}
