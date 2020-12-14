using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebService.Repository.Context.DatabaseSQL
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

        public virtual DbSet<DateDim> DateDim { get; set; }
        public virtual DbSet<DeviceDim> DeviceDim { get; set; }
        public virtual DbSet<FMeasurement> FMeasurement { get; set; }
        public virtual DbSet<LastUpdated> LastUpdated { get; set; }
        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<TimeDim> TimeDim { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:enviorment-server.database.windows.net,1433;Initial Catalog=EnviormentDatabase;Persist Security Info=False;User ID=rokasbarasa1;Password=Augis123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateDim>(entity =>
            {
                entity.HasKey(e => e.DateDimKey)
                    .HasName("PK__DateDim__6706A817EF06B0EF");

                entity.ToTable("DateDim", "DW");

                entity.HasIndex(e => e.Date)
                    .HasName("UQ__DateDim__77387D07F9C0BEFC")
                    .IsUnique();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DayOfWeekName)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.MonthName)
                    .IsRequired()
                    .HasMaxLength(9);
            });

            modelBuilder.Entity<DeviceDim>(entity =>
            {
                entity.HasKey(e => e.DeviceDimKey)
                    .HasName("PK__DeviceDi__4B3DCB8ABA0900DD");

                entity.ToTable("DeviceDim", "DW");

                entity.Property(e => e.DeviceEui)
                    .IsRequired()
                    .HasColumnName("DeviceEUI")
                    .HasMaxLength(16);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");
            });

            modelBuilder.Entity<FMeasurement>(entity =>
            {
                //entity.HasKey(e => new { e.DeviceDimKey, e.TimeDimKey, e.DateDimKey, e.MeasurementId })
                //    .HasName("PK_Measurement");
                
                entity.HasKey(e => new { e.DeviceDimKey, e.TimeDimKey, e.DateDimKey})
                    .HasName("PK_Measurement");

                entity.ToTable("F_Measurement", "DW");

                /*
                Temporarily removed because of column dispute
                TODO: Add or remove this and fix primary key method above

                entity.Property(e => e.MeasurementId)
                    .HasColumnName("MeasurementID")
                    .ValueGeneratedOnAdd();
                */

                entity.HasOne(d => d.DateDimKeyNavigation)
                    .WithMany(p => p.FMeasurement)
                    .HasForeignKey(d => d.DateDimKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__F_Measure__DateD__6521F869");

                entity.HasOne(d => d.DeviceDimKeyNavigation)
                    .WithMany(p => p.FMeasurement)
                    .HasForeignKey(d => d.DeviceDimKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__F_Measure__Devic__642DD430");

                entity.HasOne(d => d.TimeDimKeyNavigation)
                    .WithMany(p => p.FMeasurement)
                    .HasForeignKey(d => d.TimeDimKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__F_Measure__TimeD__66161CA2");
            });

            modelBuilder.Entity<LastUpdated>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LastUpdated", "DW");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");
            });

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

            modelBuilder.Entity<TimeDim>(entity =>
            {
                entity.HasKey(e => e.TimeDimKey)
                    .HasName("PK__TimeDim__D3BB96EAAAA62B49");

                entity.ToTable("TimeDim", "DW");

                entity.Property(e => e.Time).HasColumnType("time(0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
