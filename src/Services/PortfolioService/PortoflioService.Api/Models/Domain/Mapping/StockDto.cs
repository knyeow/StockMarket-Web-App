namespace PortoflioService.Api.Models.Domain.Mapping
{
    public class StockDto
    {
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
