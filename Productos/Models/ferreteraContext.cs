using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Productos.Models
{
    public partial class ferreteraContext : DbContext
    {
        public ferreteraContext()
        {
        }

        public ferreteraContext(DbContextOptions<ferreteraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Productos> Productos { get; set; } = null!;
        public virtual DbSet<Secciones> Secciones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=ferretera", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.Sku)
                    .HasName("PRIMARY");

                entity.ToTable("productos");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.IdSeccion, "FK_producto_1");

                entity.Property(e => e.Sku).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Marca).HasMaxLength(250);

                entity.Property(e => e.Nombre).HasMaxLength(300);

                entity.Property(e => e.Precio).HasPrecision(10, 2);

                entity.HasOne(d => d.IdSeccionNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdSeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkProductoSeccion");
            });

            modelBuilder.Entity<Secciones>(entity =>
            {
                entity.ToTable("secciones");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
