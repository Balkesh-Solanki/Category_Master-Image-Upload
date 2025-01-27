//using Category_Master.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Category_Master.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//        public DbSet<CategoryMst> Categories { get; set; }
//        public DbSet<SubCategoryMst> SubCategories { get; set; }
//        public DbSet<ImageMst> ImageMsts { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<CategoryMst>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(100);
//            });

//            modelBuilder.Entity<SubCategoryMst>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.Property(e => e.SubCategoryName).IsRequired().HasMaxLength(100);

//                entity.HasOne<CategoryMst>()
//                    .WithMany()
//                    .HasForeignKey(e => e.CategoryId)
//                    .OnDelete(DeleteBehavior.Cascade);
//            });


//            modelBuilder.Entity<ImageMst>(entity =>
//            {
//                entity.HasKey(e => e.Id);

//                entity.Property(e => e.ImageName)
//                    .IsRequired()
//                    .HasMaxLength(255);

//                entity.Property(e => e.ImageURL)
//                    .IsRequired()
//                    .HasMaxLength(255);

//                entity.Property(e => e.ImageType)
//                    .IsRequired()
//                    .HasMaxLength(50);

//                entity.Property(e => e.ImageMIMEType)
//                    .IsRequired()
//                    .HasMaxLength(50);

//                entity.Property(e => e.ImageSize)
//                    .IsRequired();

//                entity.Property(e => e.CreatedDate)
//                    .HasDefaultValueSql("GETDATE()");

//                entity.Property(e => e.UpdatedDate)
//                    .IsRequired(false);

//                // Prevent cascading delete on CategoryMst (No action)
//                entity.HasOne<CategoryMst>()
//                    .WithMany()
//                    .HasForeignKey(e => e.CategoryId)
//                    .OnDelete(DeleteBehavior.NoAction);

//                // Prevent cascading delete on SubCategoryMst (No action)
//                entity.HasOne<SubCategoryMst>()
//                    .WithMany()
//                    .HasForeignKey(e => e.SubCategoryId)
//                    .OnDelete(DeleteBehavior.NoAction);
//            });


//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}
