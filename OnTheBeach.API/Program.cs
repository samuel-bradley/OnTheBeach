using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using OnTheBeach.API.Services;
using OnTheBeach.Models;
using OnTheBeach.Repositories;
using System.Text.Json;

// Create the builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load flight and hotel data from JSON files
var flightJson = File.ReadAllText("Data/flights.json");
var hotelJson = File.ReadAllText("Data/hotels.json");
var options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
List<Flight> flights = JsonSerializer.Deserialize<List<Flight>>(flightJson, options) ?? new List<Flight>();
List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelJson, options) ?? new List<Hotel>();

// Register repositories with loaded data
builder.Services.AddSingleton<FlightRepository>(new FlightRepository(flights));
builder.Services.AddSingleton<HotelRepository>(new HotelRepository(hotels));

// Register HolidayService
builder.Services.AddSingleton<HolidayService>(sp =>
{
    var flightRepo = sp.GetRequiredService<FlightRepository>();
    var hotelRepo = sp.GetRequiredService<HotelRepository>();
    return new HolidayService(flightRepo, hotelRepo);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
