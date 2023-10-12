using Locations.Shared;
namespace Northwind.WebApi.Repositories;
public interface IPlacesRepository
{
    Task<SearchLocation?> CreateAsync(SearchLocation c);
    Task<IEnumerable<Result?>> Retrieve(string name);
    Task<IEnumerable<Result?>> RetrieveAll();
    string ReturnTypesAsString(Result result);

    string ReturnHtmlAttributionsAsString(Place place);
    //    Task<Place?> UpdateAsync(string id, Result c);
    //    Task<bool?> DeleteAsync(string id);
}