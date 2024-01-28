using PortoflioService.Api.Models.Domain.Mapping;
using PortoflioService.Api.Models.Domain;
using PortoflioService.Api.Repostories.PortfolioReps;

namespace PortoflioService.Api.Managers
{
    public class StockPositionManager
    {
        private readonly PortfolioContext _context;

        public StockPositionManager(PortfolioContext portfolioContext)
        {
            _context = portfolioContext;
        }

        public List<StockPositionDto> GetByPortId(string portfolioId)
        {

            List<StockPositionDto> stockPositionDtoList = positionConverter(_context.StockPositions.Where(sp => sp.PortId.Equals(portfolioId)).ToList());
            return stockPositionDtoList;
        }

        public async Task<List<StockPositionDto>> GetByPortIdAsync(string portfolioId)
        {
            var stockList = await getStockListAsync();
            List<StockPositionDto> stockPositionDtoList = positionConverter(_context.StockPositions.Where(sp => sp.PortId.Equals(portfolioId)).ToList(), stockList);
            return stockPositionDtoList;
        }

        public bool TryToGetByStockId(string portfolioId, string stockName, out StockPosition stockPosition)
        {
            var position = _context.StockPositions.Where(sp => sp.PortId.Equals(portfolioId) && sp.StockId.Equals(stockName)).FirstOrDefault();

            if (position is null)
            {
                stockPosition = null;
                return false;
            }

            stockPosition = position;
            return true;
        }

        public void AddPosition(StockPosition position)
        {
            var newPosition = position;

            var record = _context.StockPositions.Where(p => p.PortId.Equals(position.PortId) && p.StockId.Equals(position.StockId)).SingleOrDefault();

            if (record is not null)
            {
                newPosition += record;
                _context.StockPositions.Remove(record);
            }


            if (newPosition.Quantity > 0)
            {
                _context.Add(newPosition);
            }

            _context.SaveChanges();
        }

        private List<StockPositionDto> positionConverter(List<StockPosition> positionList)
        {
            List<StockPositionDto> positionDtoList = new List<StockPositionDto>();

            foreach (var position in positionList)
            {
                //var stock = _context.Stocks.Where(s => s.Id.Equals(position.StockId)).SingleOrDefault();
                //if (stock != null)
                //{
                //    positionDtoList.Add(position.ToStockPositionDto(stock));
                //}
            }
            return positionDtoList;
        }

        private List<StockPositionDto> positionConverter(List<StockPosition> positionList,List<Stock> stockList)
        {
            List<StockPositionDto> positionDtoList = new List<StockPositionDto>();

            foreach (var position in positionList)
            {
                var stock = stockList.Where(s => s.Id.Equals(position.StockId)).SingleOrDefault();
                if (stock != null)
                {
                    positionDtoList.Add(position.ToStockPositionDto(stock));
                }
            }
            return positionDtoList;
        }

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
