using PortoflioService.Api.Models.Domain.Mapping;
using PortoflioService.Api.Models.Domain;
using PortoflioService.Api.Repostories.PortfolioReps;

namespace PortoflioService.Api.Managers
{
    public class PortfolioManager
    {
        private readonly PortfolioContext _context;
        private readonly StockPositionManager _stockPositionManager;

        public PortfolioManager(PortfolioContext portfolioContext, StockPositionManager stockPositionManager)
        {
            _context = portfolioContext;
            _stockPositionManager = stockPositionManager;
        }

        public void AddPortfolio(string userId)
        {
            _context.Portfolios.Add(new Portfolio() { UserId = userId, Balance = 0 });

            _context.SaveChanges();
        }

        private Portfolio findById(string id)
        {
            var portfolio = _context.Portfolios
                .Where(p => p.PortfolioId.Equals(id))
                .SingleOrDefault();

            if (portfolio == null)
            {
                return null;
            }
            return portfolio;
        }

        public async Task<PortfolioDto> FindByIdAsync(string id)
        {
            var portfolio = _context.Portfolios
                .Where(p => p.PortfolioId.Equals(id))
                .SingleOrDefault();

            if (portfolio == null)
            {
                return null;
            }
            var portfolioDto = await portfolioConverter(portfolio);
            return portfolioDto;
        }
        public async Task<PortfolioDto> FindByUserId(string id)
        {
            var portfolio = _context.Portfolios
                .Where(p => p.UserId.Equals(id))
                .SingleOrDefault();

            if (portfolio == null)
            {
                return null;
            }
            var portfolioDto = await portfolioConverter(portfolio);
            return portfolioDto;
        }


        public void UpdateBalance(string id, decimal amount)
        {
            var port = findById(id);

            if (port is not null)
            {
                port.Balance += amount;
                _context.SaveChanges();
            }
        }

        private async  Task<PortfolioDto> portfolioConverter(Portfolio portfolio)
        {
            List<StockPositionDto> positionDtoList = await _stockPositionManager.GetByPortIdAsync(portfolio.PortfolioId);


            var portfolioDto = portfolio.ToPortfolioDto(positionDtoList);

            return portfolioDto;
        }

    }
}
