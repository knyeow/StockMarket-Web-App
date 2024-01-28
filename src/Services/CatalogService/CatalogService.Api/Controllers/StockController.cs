using CatalogService.Api.Managers;
using CatalogService.Api.Models;
using CatalogService.Api.Models.Domain;
using CatalogService.Api.Repostories.StockReps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockManager _stockManager;

        public StockController(StockManager stockManager)
        {

            _stockManager = stockManager;
        }

        // GET: api/<StockController>
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllStocks()
        {
            var stockList = _stockManager.GetAll();

            return Ok(stockList);
        }

        [HttpGet("{id}")]
        public IActionResult GetStock(string id)
        {
            var stock = _stockManager.GetById(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost()]
        public IActionResult PostStock([FromBody] Stock stock)
        {
            var result = _stockManager.AddStock(stock);

            if (result.Status == Status.Error)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(string id)
        {
            var result = _stockManager.DeleteById(id);

            if (result.Status == Status.Error)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        //Update every stock's price
        [HttpPost]
        [Route("UpdatePrices")]
        public IActionResult UpdateStockPrices()
        {
            var stockList = _stockManager.GetAll();

            foreach (var stock in stockList)
            {
                var i = (decimal)getRandomNumber(-0.2f, 0.2);
                decimal newPrice = stock.Price * (1 + i);
                _stockManager.UpdatePrice(stock.Id, newPrice);
            }

            return Ok("All Prices Updated");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchStock(string id,[FromBody]JsonPatchDocument<Stock> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest(ModelState);
            }

            var stock = _stockManager.GetById(id);

            if (stock is null)
                return NotFound();

            patchDocument.ApplyTo(stock);

            if (ModelState.IsValid)
            {
                _stockManager.SaveDatabaseChanges();
                return Ok(stock);
            }

            return BadRequest(ModelState);
        }

        private double getRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}
