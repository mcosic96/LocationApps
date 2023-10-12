using Locations.Shared;
using Newtonsoft.Json;

namespace Locations.WebApi.Services
{
    public class LocationClientService : ILocationClientService
    {
        private readonly HttpClient _httpClient;

        public LocationClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Place> GetPlaces(string uri)
        {

            var responseString = await _httpClient.GetStringAsync(uri);
            var place = JsonConvert.DeserializeObject<Place>(responseString);

            return place;
        }
    }
}
