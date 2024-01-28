using Microsoft.EntityFrameworkCore;
using PortoflioService.Api.Models.Domain;

namespace PortoflioService.Api.Repostories.PortfolioReps
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<StockPosition> StockPositions { get; set; }
    }
}
