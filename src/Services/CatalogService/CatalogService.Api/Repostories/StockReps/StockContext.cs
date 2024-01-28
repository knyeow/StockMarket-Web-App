using Microsoft.EntityFrameworkCore;
using CatalogService.Api.Models.Domain;

namespace CatalogService.Api.Repostories.StockReps
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            seedStocks(modelBuilder);
        }


        public DbSet<Stock> Stocks { get; set; }

        private static void seedStocks(ModelBuilder builder)
        {
            builder.Entity<Stock>().HasData(
                new Stock() { Name = "EXMP", Company = "Example Company", Price = 59.120m },
                new Stock() { Name = "EXMP2", Company = "Example the second Company", Price = 299.10m });
        }

    }
}
