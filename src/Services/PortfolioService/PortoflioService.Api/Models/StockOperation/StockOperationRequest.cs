namespace PortoflioService.Api.Models.StockOperation
{
    public class StockOperationRequest
    {
        public string PortfolioId { get; set; } = "XXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
        public string StockName { get; set; } = string.Empty;
        public string Operation { get; set; } = StockOperation.Buy;
        //public Decimal Price { get; set; }
        public int Quantity { get; set; }

    }

    public static class StockOperation
    {
        public const string Buy = "Buy";
        public const string Sell = "Sell";
    }
}
