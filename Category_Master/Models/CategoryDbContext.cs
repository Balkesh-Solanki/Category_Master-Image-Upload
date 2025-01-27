using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Category_Master.Models;

public partial class CategoryDbContext : DbContext
{
    public CategoryDbContext()
    {
    }

    public CategoryDbContext(DbContextOptions<CategoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryMst> CategoryMsts { get; set; }

    public virtual DbSet<ImageMst> ImageMsts { get; set; }

    public virtual DbSet<ImageMst1> ImageMsts1 { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<SubCategoryMst> SubCategoryMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-7P9C55F;Database=CategoryDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<CategoryMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07394B8EE7");

            entity.ToTable("CategoryMst");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<ImageMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageMst__3214EC0782F9AB8D");

            entity.ToTable("ImageMst");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageMimetype)
                .HasMaxLength(50)
                .HasColumnName("ImageMIMEType");
            entity.Property(e => e.ImageName).HasMaxLength(255);
            entity.Property(e => e.ImageType).HasMaxLength(50);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.ImageMsts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImageMst__Catego__4BAC3F29");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ImageMsts)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImageMst__SubCat__4CA06362");
        });

        modelBuilder.Entity<ImageMst1>(entity =>
        {
            entity.ToTable("ImageMsts");

            entity.HasIndex(e => e.CategoryId, "IX_ImageMsts_CategoryId");

            entity.HasIndex(e => e.SubCategoryId, "IX_ImageMsts_SubCategoryId");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ImageMimetype)
                .HasMaxLength(50)
                .HasColumnName("ImageMIMEType");
            entity.Property(e => e.ImageName).HasMaxLength(255);
            entity.Property(e => e.ImageType).HasMaxLength(50);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");

            entity.HasOne(d => d.Category).WithMany(p => p.ImageMst1s)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ImageMst1s)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_SubCategories_CategoryId");

            entity.Property(e => e.SubCategoryName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<SubCategoryMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCateg__3214EC0703A240DC");

            entity.ToTable("SubCategoryMst");

            entity.Property(e => e.SubCategoryName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategoryMsts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCatego__Categ__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
