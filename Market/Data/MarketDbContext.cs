using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Data
{
    public class MarketDbContext : DbContext
    {
        #region Sets
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }
        #endregion

        #region Construstors
        public MarketDbContext()
        {
            
        }

        public MarketDbContext(DbContextOptions<MarketDbContext> options) :base(options)
        {
            
        }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Optional fallback or log warning
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                      .IsRequired()                
                      .HasColumnType("decimal(18,2)");
            });
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
