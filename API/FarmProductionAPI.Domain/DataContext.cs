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

                entity.Property(e => e.Id);

                entity.Property(x => x.Code)
                      .HasMaxLength(50);
            });
          
            base.OnModelCreating(modelBuilder);
        }
    }
}
