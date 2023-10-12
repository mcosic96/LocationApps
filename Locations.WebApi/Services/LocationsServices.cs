using Locations.Shared;
using Northwind.WebApi.Repositories;


namespace Locations.WebApi.Services
{
    public class LocationsServices : ILocationsServices
    {
        private readonly IPlacesRepository _repo;
        private readonly ILocationClientService _locationsclientService;
        public Place? dataModel;

        public LocationsServices(ILocationClientService locationclientService, IPlacesRepository repo) {
            _repo = repo;
            _locationsclientService = locationclientService;
        }
        public async Task<SearchLocation> SavePlacesFromExternalApiToDatabase(string uri, SearchLocation newSearchLocation)
        {
            Place? dataModelPlace = await _locationsclientService.GetPlaces(uri);
            SearchLocation? dataModelSearchLocation = new SearchLocation();
            if (dataModelPlace == null)
            {
                return await Task.FromResult<SearchLocation>(null);
            }
            else
            {
                dataModelSearchLocation.Latitude = newSearchLocation.Latitude;
                dataModelSearchLocation.Longitude = newSearchLocation.Longitude;
                dataModelSearchLocation.Category = newSearchLocation.Category;
                dataModelSearchLocation.places = dataModelPlace;

                string? attributons = _repo.ReturnHtmlAttributionsAsString(dataModelPlace);
                dataModelSearchLocation.places.html_attr = attributons;

                List<Result>? resList = dataModelSearchLocation.places.results;
                foreach (Result res in resList)
                {
                    string types = _repo.ReturnTypesAsString(res);
                    res.categories = types;
                }

                SearchLocation? addedResponse = await _repo.CreateAsync(dataModelSearchLocation);
                if (addedResponse == null)
                {
                    return await Task.FromResult<SearchLocation>(null);
                }
                else
                {
                    return await Task.FromResult<SearchLocation>(addedResponse); ;
                }
            }
        }

        public async Task<IEnumerable<Result>> GetPlaceFromDatabase(string name)
        {
            var c = await _repo.Retrieve(name);
            if (c == null)
            {
                return await Task.FromResult<IEnumerable<Result>>(null);
            }
            return await Task.FromResult<IEnumerable<Result>>(c);
        }

        public async Task<IEnumerable<Result>> GetPlacesFromDatabase()
        {
            var c = await _repo.RetrieveAll();
            if (c == null)
            {
                return await Task.FromResult<IEnumerable<Result>>(null);
            }
            return await Task.FromResult<IEnumerable<Result>>(c);
        }

    }
}
