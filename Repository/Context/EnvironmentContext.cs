using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebService.Repository.DAO;

namespace WebService.Repository.Context.DatabaseSQL
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
        
        public virtual DbSet<OverviewModel> OverviewModel { get; set; }
        public virtual DbSet<DetailedCo2> DetailedCo2 { get; set; }
        public virtual DbSet<DetailedTemperature> DetailedTemperature { get; set; }
        public virtual DbSet<DetailedHumidity> DetailedHumidity { get; set; }

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
                entity.Property(e => e.MeasurementId).HasColumnName("measurementId");

                entity.Property(e => e.CarbonDioxide).HasColumnName("carbonDioxide");

                entity.Property(e => e.DeviceEui)
                    .IsRequired()
                    .HasColumnName("deviceEUI")
                    .HasMaxLength(16);

                entity.Property(e => e.HumidityPercentage).HasColumnName("humidityPercentage");

                entity.Property(e => e.ServoPositionPercentage).HasColumnName("servoPositionPercentage");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(e => e.DeviceEui)
                    .HasName("UQ__Room__2E2915B3732B3DFA")
                    .IsUnique();

                entity.HasIndex(e => e.SettingsId)
                    .HasName("UQ__Room__2DA7B9B3B0D367AD")
                    .IsUnique();

                entity.Property(e => e.RoomId).HasColumnName("roomId");

                entity.Property(e => e.DeviceEui)
                    .IsRequired()
                    .HasColumnName("deviceEUI")
                    .HasMaxLength(16);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.SettingsId).HasColumnName("settingsId");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.SettingsId).HasColumnName("settingsId");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("lastUpdated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PpmMax).HasColumnName("ppmMax");

                entity.Property(e => e.PpmMin).HasColumnName("ppmMin");

                entity.Property(e => e.SentToDevice)
                    .HasColumnName("sentToDevice")
                    .HasColumnType("datetime");

                entity.Property(e => e.TemperatureSetpoint).HasColumnName("temperatureSetpoint");
            });

            modelBuilder.Entity<OverviewModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<DetailedTemperature>(entity =>
            {
                entity.HasNoKey();
            });
            
            modelBuilder.Entity<DetailedHumidity>(entity =>
            {
                entity.HasNoKey();
            });
            
            modelBuilder.Entity<DetailedCo2>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
