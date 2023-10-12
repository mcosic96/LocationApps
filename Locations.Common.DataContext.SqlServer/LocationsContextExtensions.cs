using Locations.Shared;
using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
namespace Locations.Shared;
public static class LocationsContextExtensions
{
    /// <summary>
    /// Adds LocationsContext to the specified IServiceCollection. Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddLocationsContext(
    this IServiceCollection services,
    string connectionString = "Data Source=DESKTOP-GMKG8LD\\MATECOURSE;Initial Catalog=LocationsDB;Integrated Security=True; TrustServerCertificate=true;")
    {
        services.AddDbContext<LocationsContext>(options =>
        {
            options.UseSqlServer(connectionString);
            //options.LogTo(Console.WriteLine, // Console
            //new[] { Microsoft.EntityFrameworkCore
            //.Diagnostics.RelationalEventId.CommandExecuting });

        });
        return services;
    }
}