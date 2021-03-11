using AprendendoEntityFramework.MusicContext.Models;
using AprendendoEntityFramework.MusicContext.ModelsConfiguring;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AprendendoEntityFramework.MusicContext
{
    public class MusicContext : DbContext
    {
        const string _connectionString = "Data Source=DESKTOP-083D5DN\\SQLEXPRESS;Initial Catalog=MusicDB;Integrated Security=True;Pooling=False";

        public MusicContext()
        {

        }

        //Possibilita a injeção de dependencia por uma classe de serviço
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {

        }

        public DbSet<Music> Music { get; set; }
        public DbSet<Album> Album { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(_connectionString)
                    .LogConfiguration();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new MusicConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            //    t => t.GetInterfaces()
            //    .Any(i => i.IsGenericType &&
            //    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            //    && i.Namespace.Contains("MusicContext.ModelsConfiguring")));
        }
    }
}
