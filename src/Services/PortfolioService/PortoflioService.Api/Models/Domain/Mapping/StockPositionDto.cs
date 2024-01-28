namespace PortoflioService.Api.Models.Domain.Mapping
{
    public class StockPositionDto
    {
        public StockDto StockDto { get; set; } = default!;

        public decimal TotalValue { get => calculateTotalValue(StockDto.Price, Quantity); } // Toplam Ederi

        public decimal TotalCost { get; set; } = 0;  // Toplam maliyet

        public int Quantity { get; set; } = 0; // adet
        public decimal Cost { get => calculateCost(TotalCost, Quantity); } // hisse başı maliyet

        public decimal ProfitLoss { get => calculateProfit(TotalCost, StockDto.Price, Quantity); } //kâr

        public string PercentProfitLoss { get => $"{string.Format("{0:P2}",calculatePercentProfit(TotalCost, ProfitLoss))}"; } // yüzdelik kâr

        private decimal calculateTotalValue(decimal price, int quantity) => price * quantity;
        private decimal calculateCost(decimal totalCost, decimal quantity) => quantity != 0 ? totalCost / quantity : 0;
        private decimal calculateProfit(decimal totalCost, decimal price, int quantity) => (price * quantity) - totalCost;

        private decimal calculatePercentProfit(decimal totalCost, decimal profit) => profit != 0 ? (profit / totalCost) : 0;
    }
}
