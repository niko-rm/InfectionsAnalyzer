using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Colt.Infections.Library.Entities
{
    public partial class InfectionDbContext : DbContext
    {
        public InfectionDbContext()
        {
        }

        public InfectionDbContext(DbContextOptions<InfectionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventCaseDefinition> EventCaseDefinition { get; set; }
        public virtual DbSet<VirusDefinition> VirusDefinition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=tcp:s19.winhost.com;Initial Catalog=DB_94765_infection;User ID=DB_94765_infection_user;Password=Password;Integrated Security=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventCaseDefinition>(entity =>
            {
                entity.HasKey(e => e.UidCase);

                entity.Property(e => e.UidCase).ValueGeneratedNever();

                entity.Property(e => e.DateEvent).HasColumnType("date");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdVirusNavigation)
                    .WithMany(p => p.EventCaseDefinition)
                    .HasForeignKey(d => d.IdVirus)
                    .HasConstraintName("FK_EventCaseDefinition_VirusDefinition");
            });

            modelBuilder.Entity<VirusDefinition>(entity =>
            {
                entity.HasKey(e => e.IdVirus);

                entity.Property(e => e.IdVirus).ValueGeneratedNever();

                entity.Property(e => e.VirusCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VirusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
