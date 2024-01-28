using System.ComponentModel.DataAnnotations;
using CatalogService.Api.Models.Domain.Mappings;
namespace CatalogService.Api.Models.Domain
{
    public class Stock
    {
        public string Id { get; set; } = default!;

        [StringLength(5, ErrorMessage = "Stock name can't be bigger than {1}")]
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Price { get; set; }


        public StockDto ToStockDto() => new StockDto() {Name = Name, Company = Company, Price = Price };

        public Stock()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
