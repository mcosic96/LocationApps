using Locations.Shared;
using Microsoft.AspNetCore.Http.Features;
using MockHttp;
using MockHttp.Json;
using Mono.TextTemplating;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Northwind.WebApi.Repositories;
using RichardSzalay.MockHttp;

namespace Locations.WebApi.Services
{
    public class LocationsServicesMock : ILocationsServices
    {
        public async Task<SearchLocation> SavePlacesFromExternalApiToDatabase(string uri, SearchLocation newSearchLocation)
        {
            //Mockig http call to external api with a bits of data returned just to see if everything works.
            //Rest of the data in response body will be null.
            MockHttpHandler mockHttp = new MockHttpHandler();

            mockHttp
                .When(matching => matching
                    .Method("GET")
                    .RequestUri("https://exampleapi.com/api/place/*")
                )
                .Respond(with => with
                    .StatusCode(200).JsonBody(new Place{ PlaceIdentifier = 10, status = "place returned",results = new List<Result> { new Result{ name = "Bank"}, new Result { name = "Bar" } } })
                )
                .Verifiable();

            var client = new HttpClient(mockHttp);
            ILocationClientService clientService = new LocationClientService(client);

            var result = await clientService.GetPlaces("https://exampleapi.com/api/place/nearbysearch/json?location=43.716928%2C17.229781&radius=500&key=dfgdfgs");


            //Mocking saving to database call with a bits of data returned just to see if everything works.
            //Rest of the data in response body will be null.

            SearchLocation location = new SearchLocation {Latitude = 43.716928, Longitude = 17.229781 };
           location.places = result;
           var repoMock = new Mock<IPlacesRepository>();

           repoMock.Setup(t => t.CreateAsync(It.IsAny<SearchLocation>())).ReturnsAsync(location);
           return location;
        }


        //Mocking getting from database call(with name specified) with a bits of data returned just to see if everything works.
        //Rest of the data in response body will be null.
        public async Task<IEnumerable<Result>> GetPlaceFromDatabase(string name)
        {
            var repoMock = new Mock<IPlacesRepository>();
            var result = new List<Result>() { new Result { name = "Bank" } };

            repoMock.Setup(t => t.Retrieve(It.IsAny<string>())).ReturnsAsync(result);
            return await  Task.FromResult(result);
        }


        //Mocking getting from database call with a bits of data returned just to see if everything works.
        //Rest of the data in response body will be null.
        public async Task<IEnumerable<Result>> GetPlacesFromDatabase()
        {
            var repoMock = new Mock<IPlacesRepository>();
            var result = new List<Result>() { new Result { name = "Bank" }, new Result { name = "Bar" } };

            repoMock.Setup(t => t.Retrieve(It.IsAny<string>())).ReturnsAsync(result);
            return await Task.FromResult(result);
        }

    }
}
