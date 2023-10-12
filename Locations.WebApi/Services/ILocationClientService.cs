using Locations.Shared;

namespace Locations.WebApi.Services
{
    public interface ILocationClientService
    {
       Task<Place> GetPlaces(string uri);
    }
}