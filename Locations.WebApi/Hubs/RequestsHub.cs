using Microsoft.AspNetCore.SignalR;

namespace Locations.WebApi.Hubs

{
    public class RequestHub : Hub <IRequestHubClient>
    {
        //public async Task SendRequest(double? latitude, double? longitude, string? category)
        //{
        //    //await Clients.All.SendRequestWithCategory(latitude,longitude,category);
        //    //await Clients.All.SendRequest(latitude, longitude);
        //    //return Clients.All.SendAsync("ReceiveRequest", latitude,longitude,category);
        //}
    }
}
