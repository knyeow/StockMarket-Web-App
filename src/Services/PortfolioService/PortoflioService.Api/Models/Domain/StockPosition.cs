using PortoflioService.Api.Models.Domain.Mapping;
using System.ComponentModel.DataAnnotations;

namespace PortoflioService.Api.Models.Domain
{
    public class StockPosition
    {
        [Key]
        public int Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string PortId { get; set; } = default!;
        public string StockId { get; set; } = default!;


        public decimal TotalCost { get; set; } = 0;

        public int Quantity { get; set; } = 0;


        public StockPositionDto ToStockPositionDto(Stock stock)
        {
            StockPositionDto positionDto = new StockPositionDto
            {
                TotalCost = this.TotalCost,
                Quantity = this.Quantity,
                StockDto = stock.ToStockDto()
            };

            return positionDto;
        }



        public static StockPosition operator +(StockPosition lhs, StockPosition rhs)
        {
            var stockPos = lhs;
            lhs.Quantity += rhs.Quantity;
            lhs.TotalCost += rhs.TotalCost;
            return lhs;
        }

        public static StockPosition operator -(StockPosition lhs, StockPosition rhs)
        {
            var stockPos = lhs;
            lhs.Quantity -= rhs.Quantity;
            lhs.TotalCost -= rhs.TotalCost;
            return lhs;
        }

    }
}
