using CatalogService.Api.Models;
using CatalogService.Api.Models.Domain;
using CatalogService.Api.Repostories.StockReps;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CatalogService.Api.Managers
{
    public class StockManager
    {
        private readonly StockContext _context;

        public StockManager(StockContext context)
        {
            _context = context;
        }

        public List<Stock> GetAll()
        {
            var stockList = _context.Stocks.ToList();

            return stockList;
        }

        public Stock GetById(string id)
        {
            var stock = _context.Stocks
                .Where(s => s.Id.Equals(id))
                .SingleOrDefault();

            if (stock is null)
                return null;

            return stock;
        }

        public Stock GetByName(string name)
        {
            var stock = _context.Stocks
                .Where(s => s.Name.Equals(name))
                .SingleOrDefault();

            if (stock is null)
                return null;

            return stock;
        }


        public Response AddStock(Stock stock)
        {
            if (stock is null)
                return new Response { Status = Status.Error, Message = "Bad stock request" };

            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return new Response { Status = Status.Success, Message = "Stock succesfully added" };
        }


        public Response DeleteById(string id)
        {
            var stock = GetById(id);

            if (stock is null)
                return new Response { Status = Status.Error, Message = "Stock not found" };

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return new Response { Status = Status.Success, Message = "Stock succesfully removed" };
        }

        public void UpdatePrice(string stockId, decimal price)
        {
            var stock = GetById(stockId);

            if (stock is not null)
                stock.Price = price;

            _context.SaveChanges();
        }

        public void SaveDatabaseChanges()
        {
            _context.SaveChanges();
        }


    }
}
