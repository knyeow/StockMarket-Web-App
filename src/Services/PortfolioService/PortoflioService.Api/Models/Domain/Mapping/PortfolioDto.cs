namespace PortoflioService.Api.Models.Domain.Mapping
{
    public class PortfolioDto
    {
        public string PortfolioId { get; set; } = default!;
        public string UserId { get; set; } = default!;

        public decimal Balance { get; set; } // hesaptaki nakit


        public decimal PortfolioCost { get => calculateTotalCost(Positions); } // Pörtfoyun Maliyeti
        public decimal PortfolioValue { get => calculatePortfolioValue(Positions); } // pörtfoyun ederi

        public decimal ProfitLoss { get => calculateTotalProfitLoss(Positions); } // Pörtfoyun zararı kârı


        public List<StockPositionDto> Positions { get; set; } = new List<StockPositionDto>();


        private decimal calculateTotalCost(List<StockPositionDto> positions)
        {
            decimal profitLoss = 0;

            foreach (var position in positions)
                profitLoss += position.TotalCost;

            return profitLoss;
        }

        private decimal calculateTotalProfitLoss(List<StockPositionDto> positions)
        {
            decimal profitLoss = 0;

            foreach (var position in positions)
                profitLoss += position.ProfitLoss;

            return profitLoss;
        }

        private decimal calculatePortfolioValue(List<StockPositionDto> positions)
        {
            decimal value = 0;

            foreach (var position in positions)
                value += position.TotalValue;

            return value;
        }


    }
}
