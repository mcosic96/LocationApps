using System.Net.Http.Headers; // MediaTypeWithQualityHeaderValue
using Locations.Shared;
using Locations.WebApi.Repositories;
using Northwind.WebApi.Repositories;
using Microsoft.AspNetCore.ResponseCompression;
using Locations.WebApi.Hubs;
using Locations.WebApi.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.HttpLogging;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//This is pre-emptive dependency resolution an is useful in this case for debug mode, if scenario requires on-demand runtime dependency resolution 
//then design patterns like factory, proxy or adapter are the way to conditionally resolve dependencies.
if (Debugger.IsAttached)
{
    builder.Services.AddScoped<ILocationsServices, LocationsServicesMock>();
}
else
{
    builder.Services.AddScoped<ILocationsServices, LocationsServices>();
}

//#if DEBUG
//builder.Services.AddScoped<ILocationsServices, LocationsServicesMock>();
//#else
//builder.Services.AddScoped<ILocationsServices, LocationsServices>();
//#endif


builder.Services.AddHttpClient<ILocationClientService, LocationClientService>(client =>
{
    client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
});


// if you are using SQL Server
string? sqlServerConnection = builder.Configuration
.GetConnectionString("LocationsConnection");
if (sqlServerConnection is null)
{
    Console.WriteLine("SQL Server database connection string is missing!");
}
else
{
    builder.Services.AddLocationsContext(sqlServerConnection);
}
builder.Services.AddScoped<IPlacesRepository, PlacesRepository>();
builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//UseHttpsRedirection middleware redirects incoming HTTP requests to HTTPS, ensuring that all communication between the client and server is encrypted.
app.UseHttpsRedirection();


//Authorization Middleware authorizes a user to access secure resources. Use it in order to restrict specific parts of the application.
app.UseAuthorization();

app.MapControllers();
app.MapHub<RequestHub>("/requesthub");

app.Run();
