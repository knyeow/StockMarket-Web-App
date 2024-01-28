using PortoflioService.Api.Managers;
using PortoflioService.Api.Models.Domain;
using PortoflioService.Api.Models.StockOperation;

namespace PortoflioService.Api.Services
{
    public class StockOperationService
    {
        private readonly StockPositionManager _positionManager;
        private readonly PortfolioManager _portfolioManager;

        public StockOperationService(StockPositionManager positionManager, PortfolioManager portfolioManager)
        {
            _positionManager = positionManager;
            _portfolioManager = portfolioManager;
        }

        public async Task<StockOperationResponse> AddStockOperationAsync(StockOperationRequest request)
        {
            var portfolio = await _portfolioManager.FindByIdAsync(request.PortfolioId);

            var stockList = await getStockListAsync();

            if(stockList is null)
            {
                return new StockOperationResponse() { Result = StockOperationResult.Error, Message = "Can not reach stock list" };
            }

            var stock = stockList.Where(s => s.Name.Equals(request.StockName)).SingleOrDefault();


            if (portfolio is null)
            {
                return new StockOperationResponse() { Result = StockOperationResult.Error, Message = "Portfolio not found" };
            }

            if (stock is null)
            {
                return new StockOperationResponse() { Result = StockOperationResult.Error, Message = "Stock not found" };
            }

            var quantity = request.Quantity;
            var totalCost = request.Quantity * stock.Price;
            var newPosition = new StockPosition() { UserId = portfolio.UserId, PortId = portfolio.PortfolioId, StockId = stock.Id, Quantity = request.Quantity, TotalCost = request.Quantity * stock.Price };

            if (request.Operation == StockOperation.Buy)
            {
                if (!hasEnoughBalance(portfolio.Balance, stock.Price, request.Quantity))
                {
                    return new StockOperationResponse() { Result = StockOperationResult.Error, Message = "Not enough balance" };
                }

                _portfolioManager.UpdateBalance(request.PortfolioId, -totalCost);
            }

            if (request.Operation == StockOperation.Sell)
            {


                if (!_positionManager.TryToGetByStockId(request.PortfolioId, stock.Id, out StockPosition position))
                {
                    return new StockOperationResponse() { Result = StockOperationResult.Error, Message = "You can't sell something you don't have" };
                }

                if (!hasEnoughQuantity(position.Quantity, request.Quantity))
                {
                    return new StockOperationResponse() { Result = StockOperationResult.Error, Message = "You don't have enough amount of stock" };
                }

                newPosition.Quantity *= -1;
                newPosition.TotalCost *= -1;

                _portfolioManager.UpdateBalance(request.PortfolioId, totalCost);

            }

            _positionManager.AddPosition(newPosition);

            return new StockOperationResponse() { Result = StockOperationResult.Success, Message = "Position succesfully added" };
        }

        private bool hasEnoughBalance(decimal balance, decimal price, decimal quantity) => balance > price * quantity;

        private bool hasEnoughQuantity(decimal currentQuantity, decimal quantity) => currentQuantity >= quantity;

        private async Task<List<Stock>?> getStockListAsync()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:8000");

            List<Stock>? stocks = await client
                .GetFromJsonAsync<List<Stock>>($"apigateway/Stock/All");

            return stocks;

        }
    }
}
