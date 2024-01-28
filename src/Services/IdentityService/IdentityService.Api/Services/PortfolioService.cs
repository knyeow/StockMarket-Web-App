using IdentityService.Api.Interface;
using System.Text.Json;
using System.Text;
using IdentityService.Api.Models.Identity;

namespace IdentityService.Api.Services
{
    public class PortfolioService : IPortfolioService
    {
        static string baseAddress = "https://localhost:8000/";
        public void AddPortfolioToUser(string userId)
        {
            AddPortfolio(userId);
        }

        private void AddPortfolio(string userId)
        {
            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(baseAddress);


            var serializeCategory = JsonSerializer.Serialize(userId);

            StringContent stringContent = new StringContent(serializeCategory, Encoding.UTF8, "application/json");

            var result = httpClient.PostAsync("apigateway/Portfolio", stringContent).Result;
        
        }

        private class Portfolio
        {
            public string PortfolioId { get; set; }
            public string UserId { get; set; } = string.Empty;
            public decimal Balance { get; set; } = 0;

            public Portfolio() { PortfolioId = Guid.NewGuid().ToString(); }
        }
    }

}
