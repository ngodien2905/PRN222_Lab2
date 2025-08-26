using System;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public partial class GameHubContext : DbContext
{
    public GameHubContext()
    {
    }

    public GameHubContext(DbContextOptions<GameHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameCategory> GameCategories { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerGame> PlayerGames { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwGameRegistrationCount> VwGameRegistrationCounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=GameHub;User Id=sa;Password=12345;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.DeveloperId).HasName("PK__Develope__DE084CF101269791");

            entity.ToTable("Developer");

            entity.HasIndex(e => e.DeveloperName, "UQ__Develope__08E3F54D39F139D9").IsUnique();

            entity.Property(e => e.DeveloperName).HasMaxLength(200);
            entity.Property(e => e.Website).HasMaxLength(250);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__2AB897FD57AD42AB");

            entity.ToTable("Game");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Games)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Game__CategoryId__300424B4");

            entity.HasOne(d => d.Developer).WithMany(p => p.Games)
                .HasForeignKey(d => d.DeveloperId)
                .HasConstraintName("FK__Game__DeveloperI__2F10007B");
        });

        modelBuilder.Entity<GameCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__GameCate__19093A0B2D38028A");

            entity.ToTable("GameCategory");

            entity.HasIndex(e => e.CategoryName, "UQ__GameCate__8517B2E00AEE7830").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74C801CB4D7C");

            entity.ToTable("Player");

            entity.HasIndex(e => e.UserId, "UQ__Player__1788CC4DEFB52EF8").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Player__536C85E416270C35").IsUnique();

            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.User).WithOne(p => p.Player)
                .HasForeignKey<Player>(d => d.UserId)
                .HasConstraintName("FK__Player__UserId__34C8D9D1");
        });

        modelBuilder.Entity<PlayerGame>(entity =>
        {
            entity.HasKey(e => new { e.PlayerId, e.GameId });

            entity.ToTable("PlayerGame");

            entity.HasIndex(e => e.GameId, "IX_PlayerGame_GameId");

            entity.Property(e => e.RegisteredAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Game).WithMany(p => p.PlayerGames)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK_PlayerGame_Game");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerGames)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_PlayerGame_Player");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CC05B0280");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105344C393DE4").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(u => u.RoleId)
                 .HasConversion<byte>()
                 .HasColumnName("Role")
                 .ValueGeneratedNever();
        });

        modelBuilder.Entity<VwGameRegistrationCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_GameRegistrationCounts");

            entity.Property(e => e.Title).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
