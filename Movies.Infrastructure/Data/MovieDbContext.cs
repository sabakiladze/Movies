using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Data
{
    public  class MovieDbContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Directors { get; set; }
        public DbSet<Studio> Genres { get; set; }
        public DbSet<StudioDetails> StudioDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var _connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(builder =>
            {
                builder.ToTable("Movies");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Title).HasColumnType("varchar(150)").IsRequired();

                builder.HasMany(m => m.Actors)
                      .WithMany(a => a.Movies);
            });


            modelBuilder.Entity<Country>(builder =>
            {
                builder.ToTable("Countries");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasDefaultValueSql("NEWID()"); // თუ Guid-ს იყენებ ID-დ
                builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();

                builder.HasMany(c => c.Studios)  // ქვეყნის ფროფერთი Studio უკავშირდება სტუდიოს ფროფერთის Country. (c => c.Studios)  c-country object, s-studio property of c. ( s => s.Country)    s is object of Studio, and Country is iths property.
                      .WithOne(s => s.Country)/// ეს ფროფერთია და არა კლასი.
                      .HasForeignKey(s => s.CountryId);    //სტუდიოს ველი.
            });

            modelBuilder.Entity<Actor>(builder =>
            {
                builder.ToTable("Actors");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.FirstName).HasColumnType("varchar(100)").IsRequired();
                builder.Property(x => x.LastName).HasColumnType("varchar(100)").IsRequired();
            });

            modelBuilder.Entity<Studio>(builder =>
            {
                builder.ToTable("Studios");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();

                builder.HasOne(x =>x.StudioDetails)
                       .WithOne(x=> x.Studio)
                       .HasForeignKey<StudioDetails>(sd => sd.StudioId);

                builder.HasMany(s => s.Movies)
                       .WithOne(m => m.Studio)
                       .HasForeignKey(m => m.StudioId);
            });

            modelBuilder.Entity<StudioDetails>(builder =>
            {
                builder.ToTable("StudioDetails");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.LicenseNumber).IsRequired();
            });


        }

    }
}
