

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Infrastructure
{
    public class SkiNetDbContext : DbContext
    {
        #region Fields

        #endregion

        #region Propperties

        #endregion

        #region Constructors
        public SkiNetDbContext(DbContextOptions<SkiNetDbContext> option) : base(option) { }
        #endregion

        #region DbSets  
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        /// <summary>
        /// Which help us to dyanamically configure the IEntityTypeConfiguration from this assembly
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        #endregion
    }
}
