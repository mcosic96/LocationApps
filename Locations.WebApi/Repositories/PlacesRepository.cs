using Locations.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Northwind.WebApi.Repositories;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Locations.WebApi.Repositories
{
    public class PlacesRepository: IPlacesRepository
    {
        private LocationsContext db;
        public PlacesRepository(LocationsContext injectedContext)
        {
            db = injectedContext;
        }

        public async Task<SearchLocation?> CreateAsync(SearchLocation c)
        {

            EntityEntry<SearchLocation> added = await db.SearchLocations.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if (affected > 0)
            {
                return c;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Result?>> Retrieve(string name)
        {
            var c = await db.Results.Where(r => r.name.Contains(name))
                .Include(r => r.geometry)
                .Include(r => r.geometry.location)
                .Include(r => r.geometry.viewport)
                .Include(r => r.geometry.viewport.southwest)
                .Include(r => r.geometry.viewport.northeast)
                .Include(r => r.opening_hours)
                .Include(r => r.photos)
                .Include(r => r.plus_code).ToListAsync();
            return c;
        }

        public async Task<IEnumerable<Result?>> RetrieveAll()
        {

            var c = await db.Results
                .Include(r => r.geometry)
                .Include(r => r.geometry.location)
                .Include(r => r.geometry.viewport)
                .Include(r => r.geometry.viewport.southwest)
                .Include(r => r.geometry.viewport.northeast)
                .Include(r => r.opening_hours)
                .Include(r => r.photos)
                .Include(r => r.plus_code).ToListAsync();
            return c;
        }

        public string ReturnTypesAsString(Result result)
        {

            List<string>? types = result.types;
            string joinedTypes = String.Join(",", types); ;
                
            return joinedTypes;

        }

        public string ReturnHtmlAttributionsAsString(Place place)
        {

            List<string>? attributions = place.html_attributions;
            if (attributions == null)
            {
                return null;
            }
            else
            {
                string joinedAttributions = String.Join(",", attributions); ;
                return joinedAttributions;
            }
        }
    }
}
