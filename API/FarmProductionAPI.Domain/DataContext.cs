using FarmProductionAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // filter deleted item
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isDeletedProperty = entityType.FindProperty("IsSoftDeleted");
                if (isDeletedProperty?.PropertyInfo != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var filter = Expression.Lambda(
                        Expression.Equal(Expression.Property(parameter, isDeletedProperty.PropertyInfo), Expression.Constant(false)),
                        parameter);

                    entityType.SetQueryFilter(filter);
                }
            }

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.HasKey(e => e.Id);

                entity.Property(x => x.Code)
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("ProductAttribute");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.HasKey(e => e.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
