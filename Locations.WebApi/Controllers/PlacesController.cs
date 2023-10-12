using Microsoft.AspNetCore.Mvc;
using Locations.Shared;
using Microsoft.AspNetCore.SignalR;
using Locations.WebApi.Hubs;
using Locations.WebApi.Services;
using System.Diagnostics;

namespace Locations.WebApi.Controllers
{

    public class SearchParams { public double? Latitude { get; set; } public double? Longitude { get; set; } public string? Category { get; set; } }
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<RequestHub, IRequestHubClient> _requestHubContext;
        private readonly ILocationsServices _locationsServices;

        public PlacesController(ILocationsServices locationsServices, IConfiguration config, IHubContext<RequestHub, IRequestHubClient> requestHubContext)
        {
            _requestHubContext = requestHubContext;
            _configuration = config;
            _locationsServices = locationsServices;
        }

        [HttpPost]
        public async Task<IActionResult> GetPlaces([FromBody] SearchParams centerLocation)
        {

            string uri;
            string? ApiKey = _configuration.GetValue<string>("ConnectionStrings:ApiKey");
            SearchLocation newSearchLocation;

            if ((centerLocation.Latitude is null) || (centerLocation.Longitude is null))
            {
                return BadRequest("Please enter latitude and longitude at least.");
            }
            else
            {
                if (string.IsNullOrEmpty(centerLocation.Category))
                {
                    await _requestHubContext.Clients.All.SendRequest(centerLocation.Latitude, centerLocation.Longitude);
                    uri = $"api/place/nearbysearch/json?location={centerLocation.Latitude}%2C{centerLocation.Longitude}&radius=500&key={ApiKey}";
                    newSearchLocation = new SearchLocation { Longitude = centerLocation.Longitude, Latitude = centerLocation.Latitude};
                }
                else
                {
                    await _requestHubContext.Clients.All.SendRequestWithCategory(centerLocation.Latitude, centerLocation.Longitude, centerLocation.Category);
                    uri = $"api/place/nearbysearch/json?location={centerLocation.Latitude}%2C{centerLocation.Longitude}&radius=500&key={ApiKey}&type={centerLocation.Category}";
                    newSearchLocation = new SearchLocation {Longitude = centerLocation.Longitude, Latitude = centerLocation.Latitude, Category = centerLocation.Category };
                }
            }

            var place = await _locationsServices.SavePlacesFromExternalApiToDatabase(uri, newSearchLocation);


            if (place != null)
            {
                return Ok(place);
            }
            else
            {
                return BadRequest("Could not get data from Google Api (check if Uri is correct) or saving to database failed.");
            }
        }

        // GET: api/places/name
        [HttpGet("{name}", Name = nameof(GetPlaceWithName))] // named route
        [ProducesResponseType(200, Type = typeof(IQueryable<Place?>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlaceWithName(string name)
        {

            var c = await _locationsServices.GetPlaceFromDatabase(name);

            if (c == null)
            {
                return NotFound(); // 404 Resource not founde
            }
            return Ok(c); // 200 OK with place in body
        }

        // GET: api/places/
        [HttpGet(Name = nameof(GetPlace))] // named route
        [ProducesResponseType(200, Type = typeof(Result))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlace()
        {
            var c = await _locationsServices.GetPlacesFromDatabase();

            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return Ok(c); // 200 OK with place in body
        }
    }
    }
