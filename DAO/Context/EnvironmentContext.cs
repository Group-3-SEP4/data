using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebService.Models;

namespace WebService.DAO
{
    public partial class EnvironmentContext : DbContext
    {
        public EnvironmentContext()
        {
        }

        public EnvironmentContext(DbContextOptions<EnvironmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:enviorment-server.database.windows.net,1433;Initial Catalog=EnviormentDatabase;Persist Security Info=False;User ID=rokasbarasa1;Password=Augis123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.HasKey(e => new { e.DeviceId, e.MeasurementId });

                entity.Property(e => e.DeviceId)
                    .HasColumnName("deviceId")
                    .HasMaxLength(16);

                entity.Property(e => e.MeasurementId)
                    .HasColumnName("measurementId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CarbonDioxide).HasColumnName("carbonDioxide");

                entity.Property(e => e.HumidityPercentage).HasColumnName("humidityPercentage");

                entity.Property(e => e.ServoPositionPercentage).HasColumnName("servoPositionPercentage");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("roomId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.SettingsId).HasColumnName("settingsId");

                entity.HasOne(d => d.Settings)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.SettingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Settings");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.SettingsId).HasColumnName("settingsId");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("lastUpdated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PpmMax).HasColumnName("ppmMax");

                entity.Property(e => e.PpmMin).HasColumnName("ppmMin");

                entity.Property(e => e.TemperatureSetpoint).HasColumnName("temperatureSetpoint");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
