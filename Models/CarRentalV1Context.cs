using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRentalApi.Models
{
    public partial class CarRentalV1Context : DbContext
    {
        public CarRentalV1Context()
        {
        }

        public CarRentalV1Context(DbContextOptions<CarRentalV1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<FuelType> FuelTypes { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=CarRentalApiContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LicenseNo).HasMaxLength(50);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Car_Color");

                entity.HasOne(d => d.FuelType)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.FuelTypeId)
                    .HasConstraintName("FK_Car_FuelType");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Car_Location");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColorDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ColorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FuelType>(entity =>
            {
                entity.ToTable("FuelType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FuelTypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Zip).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
