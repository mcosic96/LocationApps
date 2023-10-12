using Locations.Shared;

namespace Locations.WebApi.Services
{
    public interface ILocationsServices
    {
        Task<SearchLocation> SavePlacesFromExternalApiToDatabase(string uri, SearchLocation newSearchLocation);
        Task<IEnumerable<Result>> GetPlaceFromDatabase(string name);
        Task<IEnumerable<Result>> GetPlacesFromDatabase();
    }
}