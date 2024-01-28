using PortoflioService.Api.Models.Domain;

namespace PortoflioService.Api.Models.StockOperation
{
    public class StockOperationResponse
    {
        public string Result { get; set; } = string.Empty;
        public string Message = string.Empty;
        public StockPosition Position { get; set; } = new StockPosition();
        public DateTime OperationTime { get; set; } = DateTime.UtcNow;


    }

    public static class StockOperationResult
    {
        public const string Success = "Success";
        public const string Error = "Error";
    }
}
