using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Store.DataAccess.Models.IdentityModels;

namespace Store.DataAccess.Models
{
    public partial class MarketCoreContext : IdentityDbContext<ApplicationUser>
    {
        public MarketCoreContext()
        {
        }

        public MarketCoreContext(DbContextOptions<MarketCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-8FDI80B\\SQLEXPRESS;Database=MarketCore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.ParentId })
                    .HasName("category_index")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(80);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Price).HasMaxLength(10);
            });
        }
    }
}
