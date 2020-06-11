using APDB_AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APDB_AdvertApi.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.Id).ValueGeneratedOnAdd();
                entity.ToTable("RefreshToken");
            });

            builder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.IdClient);
                entity.Property(c => c.IdClient).ValueGeneratedOnAdd();
                entity.ToTable("Client");
                entity.HasMany(c => c.Campaigns)
                    .WithOne(ca => ca.Client)
                    .HasForeignKey(ca => ca.IdClient)
                    .IsRequired();
            });

            builder.Entity<Building>(entity =>
            {
                entity.HasKey(b => b.IdBuilding);
                entity.Property(b => b.IdBuilding).ValueGeneratedOnAdd();
                entity.ToTable("Building");
                entity.HasMany(d => d.FromCampaigns)
                    .WithOne(ca => ca.FromBuilding)
                    .HasForeignKey(ca => ca.FromIdBuilding)
                    .IsRequired();
                entity.HasMany(d => d.ToCampaigns)
                    .WithOne(ca => ca.ToBuilding)
                    .HasForeignKey(ca => ca.ToIdBuilding)
                    .IsRequired();
            });

            builder.Entity<Campaign>(entity =>
            {
                entity.HasKey(c => c.IdCampaign);
                entity.Property(c => c.IdCampaign).ValueGeneratedOnAdd();
                entity.ToTable("Campaign");
                entity.HasMany(c => c.Banners)
                    .WithOne(b => b.Campaign)
                    .HasForeignKey(b => b.IdCampaign)
                    .IsRequired();
            });

            builder.Entity<Banner>(entity =>
            {
                entity.HasKey(b => b.IdAdvertisement);
                entity.Property(b => b.IdAdvertisement).ValueGeneratedOnAdd();
                entity.ToTable("Banner");
            });
        }
    }
}
