using PortoflioService.Api.Models.Domain.Mapping;
using System.ComponentModel.DataAnnotations;

namespace PortoflioService.Api.Models.Domain
{
    public class Portfolio
    {

        [Key]
        public string PortfolioId { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public decimal Balance { get; set; }

        public PortfolioDto ToPortfolioDto(List<StockPositionDto> positions)
        {
            PortfolioDto portfolioDto = new PortfolioDto
            {
                UserId = this.UserId,
                PortfolioId = this.PortfolioId,
                Balance = this.Balance,
                Positions = positions
            };

            return portfolioDto;
        }


        public Portfolio() { PortfolioId = Guid.NewGuid().ToString(); }
    }
}
