using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameShowcase.Models
{
    public partial class GameDbContext : DbContext
    {
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Leader> Leaders { get; set; }
        public virtual DbSet<Screenshot> Screenshots { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Password).HasColumnType("varchar(16)");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CoverUrl)
                    .HasColumnType("varchar(1025)")
                    .HasDefaultValueSql("http://omj7nb0gz.bkt.clouddn.com/game_cover.png");

                entity.Property(e => e.DemoUrl).HasColumnType("varchar(1025)");

                entity.Property(e => e.Description).HasColumnType("varchar(4098)");

                entity.Property(e => e.Genre)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("Unknown");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("Untitled");

                entity.HasOne(d => d.Leader)
                    .WithOne(p => p.Game)
                    .HasForeignKey<Game>(d => d.Id)
                    .HasConstraintName("FK_Game_Leader");
            });

            modelBuilder.Entity<Leader>(entity =>
            {
                entity.ToTable("leader");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CoMembers).HasColumnType("varchar(50)");

                entity.Property(e => e.Email).HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(16)")
                    .HasDefaultValueSql("111111");

                entity.Property(e => e.StudentNumber)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Year).HasColumnType("int(4)");
            });

            modelBuilder.Entity<Screenshot>(entity =>
            {
                entity.ToTable("screenshot");

                entity.HasIndex(e => e.GameId)
                    .HasName("FK_Screenshot_Game");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.GameId).HasColumnType("int(11)");

                entity.Property(e => e.Url).HasColumnType("varchar(1025)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Screenshots)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_Screenshot_Game");
            });
        }
    }
}