using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using OnTheBeach.Models;

namespace OnTheBeach.Tests
{
    public class HotelTests
    {
        [Fact]
        public void Hotel_Deserialization_Succeeds()
        {
            var json = @"
            [
                {
                    ""id"": 1,
                    ""name"": ""Iberostar Grand Portals Nous"",
                    ""arrival_date"": ""2022-11-05"",
                    ""price_per_night"": 100,
                    ""local_airports"": [""TFS""],
                    ""nights"": 7
                },
                {
                    ""id"": 2,
                    ""name"": ""Laguna Park 2"",
                    ""arrival_date"": ""2022-11-05"",
                    ""price_per_night"": 50,
                    ""local_airports"": [""TFS""],
                    ""nights"": 7
                }
            ]";

            var options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            var hotels = JsonSerializer.Deserialize<List<Hotel>>(json, options);

            Assert.NotNull(hotels);
            Assert.Equal(2, hotels.Count);

            Assert.Equal(1, hotels[0].Id);
            Assert.Equal("Iberostar Grand Portals Nous", hotels[0].Name);
            Assert.Equal(new LocalDate(2022, 11, 5), hotels[0].ArrivalDate);
            Assert.Equal(100, hotels[0].PricePerNight);
            Assert.Equal(new List<string> { "TFS" }, hotels[0].LocalAirports);
            Assert.Equal(7, hotels[0].Nights);

            Assert.Equal(2, hotels[1].Id);
            Assert.Equal("Laguna Park 2", hotels[1].Name);
            Assert.Equal(new LocalDate(2022, 11, 5), hotels[1].ArrivalDate);
            Assert.Equal(50, hotels[1].PricePerNight);
            Assert.Equal(new List<string> { "TFS" }, hotels[1].LocalAirports);
            Assert.Equal(7, hotels[1].Nights);
        }
    }
}
