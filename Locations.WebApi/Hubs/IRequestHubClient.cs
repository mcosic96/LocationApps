namespace Locations.WebApi.Hubs
{
    public interface IRequestHubClient
    {
        Task SendRequestWithCategory(double? latitude, double? longitude, string? category);
        Task SendRequest(double? latitude, double? longitude);
    }
}
