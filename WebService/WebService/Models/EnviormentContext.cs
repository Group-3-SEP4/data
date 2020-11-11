using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebService.Models
{
    public partial class EnviormentContext : DbContext
    {
        public EnviormentContext()
        {
        }

        public EnviormentContext(DbContextOptions<EnviormentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarbonDioxideReading> CarbonDioxideReading { get; set; }
        public virtual DbSet<HumidityReading> HumidityReading { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<TemperatureReading> TemperatureReading { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:enviorment-server.database.windows.net,1433;Initial Catalog=EnviormentDatabase;Persist Security Info=False;User ID=rokasbarasa1;Password=Augis123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarbonDioxideReading>(entity =>
            {
                entity.HasKey(e => e.CarbrId);

                entity.Property(e => e.CarbrId).HasColumnName("carbr_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.CarbonDioxideReading)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_carbondDioxide_room");
            });

            modelBuilder.Entity<HumidityReading>(entity =>
            {
                entity.HasKey(e => e.HumrId);

                entity.Property(e => e.HumrId).HasColumnName("humr_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.HumidityReading)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_humidity_room");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.SettingsId).HasColumnName("settings_id");

                entity.HasOne(d => d.Settings)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.SettingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_room_settings");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.SettingsId).HasColumnName("settings_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("lastUpdated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PpmMax).HasColumnName("ppmMax");

                entity.Property(e => e.PpmMin).HasColumnName("ppmMin");

                entity.Property(e => e.TemperatureSetpoint).HasColumnName("temperatureSetpoint");
            });

            modelBuilder.Entity<TemperatureReading>(entity =>
            {
                entity.HasKey(e => e.TemprId);

                entity.Property(e => e.TemprId).HasColumnName("tempr_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.TemperatureReading)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_temperature_room");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
