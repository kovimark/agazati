using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace csatahajok.Models;

public partial class CsatahajokContext : DbContext
{
    public CsatahajokContext()
    {
    }

    public CsatahajokContext(DbContextOptions<CsatahajokContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Csatum> Csata { get; set; }

    public virtual DbSet<Hajo> Hajos { get; set; }

    public virtual DbSet<Kimenet> Kimenets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=csatahajok;user=root;password=;ssl mode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Csatum>(entity =>
        {
            entity.HasKey(e => e.Nev).HasName("PRIMARY");

            entity.ToTable("csata");

            entity.Property(e => e.Nev)
                .HasMaxLength(32)
                .HasColumnName("nev");
            entity.Property(e => e.Befejezes)
                .HasColumnType("date")
                .HasColumnName("befejezes");
            entity.Property(e => e.Kezdes)
                .HasColumnType("date")
                .HasColumnName("kezdes");
        });

        modelBuilder.Entity<Hajo>(entity =>
        {
            entity.HasKey(e => e.Nev).HasName("PRIMARY");

            entity.ToTable("hajo");

            entity.Property(e => e.Nev)
                .HasMaxLength(32)
                .HasColumnName("nev");
            entity.Property(e => e.AgyukSzama)
                .HasColumnType("int(2)")
                .HasColumnName("agyukSzama");
            entity.Property(e => e.Felavatva)
                .HasColumnType("int(4)")
                .HasColumnName("felavatva");
            entity.Property(e => e.Kaliber)
                .HasColumnType("int(2)")
                .HasColumnName("kaliber");
            entity.Property(e => e.Osztaly)
                .HasMaxLength(32)
                .HasColumnName("osztaly");
            entity.Property(e => e.Vizkiszoritas)
                .HasColumnType("int(11)")
                .HasColumnName("vizkiszoritas");
        });

        modelBuilder.Entity<Kimenet>(entity =>
        {
            entity.HasKey(e => new { e.Hajo, e.Csata }).HasName("PRIMARY");

            entity.ToTable("kimenet");

            entity.HasIndex(e => e.Csata, "csata");

            entity.Property(e => e.Hajo)
                .HasMaxLength(32)
                .HasColumnName("hajo");
            entity.Property(e => e.Csata)
                .HasMaxLength(32)
                .HasColumnName("csata");
            entity.Property(e => e.Eredmeny)
                .HasMaxLength(16)
                .HasColumnName("eredmeny");

            entity.HasOne(d => d.CsataNavigation).WithMany(p => p.Kimenets)
                .HasForeignKey(d => d.Csata)
                .HasConstraintName("kimenet_ibfk_2");

            entity.HasOne(d => d.HajoNavigation).WithMany(p => p.Kimenets)
                .HasForeignKey(d => d.Hajo)
                .HasConstraintName("kimenet_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
