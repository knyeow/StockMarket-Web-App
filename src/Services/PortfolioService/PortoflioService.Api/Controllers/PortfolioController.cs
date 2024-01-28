using Microsoft.AspNetCore.Mvc;
using PortoflioService.Api.Managers;
using PortoflioService.Api.Models.Domain;
using PortoflioService.Api.Models.StockOperation;
using PortoflioService.Api.Services;
using System.Runtime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortoflioService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioManager _portfolioManager;
        private readonly StockOperationService _stockOperationService;


        public PortfolioController(PortfolioManager portfolioManager, StockOperationService stockOperationService)
        {
            _portfolioManager = portfolioManager;
            _stockOperationService = stockOperationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var portfolio = await _portfolioManager.FindByIdAsync(Id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(portfolio);
        }

        [HttpGet("main/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var portfolio = await _portfolioManager.FindByUserId(userId);

            if (portfolio == null)
            { 
                return NotFound();
            }

            return Ok(portfolio);
        }

        [HttpPost]
        public IActionResult PostPortfolio([FromBody] string userId)
        {
            _portfolioManager.AddPortfolio(userId);
            return Ok();
        }

        [HttpPost("AddStockPosition")]
        public async Task<IActionResult> PostStockPosition([FromBody] StockOperationRequest request)
        {
            var response = await _stockOperationService.AddStockOperationAsync(request);

            if (response.Result == "Error")
            {
                return BadRequest(response.Message);
            }

            return Ok();
        }

        [HttpPost("AddBalance/{portId}/{balance}")]
        public async Task<IActionResult> PostBalance(string portId, decimal balance)
        {
            var portfolio = await _portfolioManager.FindByIdAsync(portId);

            if (portfolio is null)
            {
                return NotFound();
            }

            _portfolioManager.UpdateBalance(portId, balance);
            return Ok();
        }




    }
}
