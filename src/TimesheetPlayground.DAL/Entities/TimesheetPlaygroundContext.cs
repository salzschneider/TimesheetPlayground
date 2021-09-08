using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TimesheetPlayground.DAL.Entities
{
    public partial class TimesheetPlaygroundContext : DbContext
    {
        public TimesheetPlaygroundContext()
        {
        }

        public TimesheetPlaygroundContext(DbContextOptions<TimesheetPlaygroundContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkedHour> WorkedHours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-9LMUC9L\\SQLEXPRESS;Database=TimesheetPlayground;User Id=TSPlayDBUser;Password=aYnAkvAB3IhBg41bxex6");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.ToTable("Timesheet");

                entity.HasIndex(e => new { e.UserId, e.StartDate }, "IX_UserId_StartDate");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Timesheet_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WorkedHour>(entity =>
            {
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.WorkedHours)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkedHours_Projects");

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.WorkedHours)
                    .HasForeignKey(d => d.TimesheetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkedHours_Timesheet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
