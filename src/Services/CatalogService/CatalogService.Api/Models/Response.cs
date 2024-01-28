namespace CatalogService.Api.Models
{
    public class Response
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public static class Status
    {
        public const string Success = "Success";
        public const string Error = "Error";
    }
}
