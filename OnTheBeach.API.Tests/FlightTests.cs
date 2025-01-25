using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using OnTheBeach.Models;

namespace OnTheBeach.Tests
{
    public class FlightTests
    {
        [Fact]
        public void Flight_Deserialization_YieldsCorrectFlights()
        {
            var json = @"
            [
                {
                    ""id"": 1,
                    ""airline"": ""First Class Air"",
                    ""from"": ""MAN"",
                    ""to"": ""TFS"",
                    ""price"": 470,
                    ""departure_date"": ""2023-07-01""
                },
                {
                    ""id"": 2,
                    ""airline"": ""Oceanic Airlines"",
                    ""from"": ""MAN"",
                    ""to"": ""AGP"",
                    ""price"": 245,
                    ""departure_date"": ""2023-07-01""
                }
            ]";

            // Configure System.Text.Json with NodaTime support
            var options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            var flights = JsonSerializer.Deserialize<List<Flight>>(json, options);

            Assert.NotNull(flights);
            Assert.Equal(2, flights.Count);

            Assert.Equal(1, flights[0].Id);
            Assert.Equal("First Class Air", flights[0].Airline);
            Assert.Equal("MAN", flights[0].From);
            Assert.Equal("TFS", flights[0].To);
            Assert.Equal(470, flights[0].Price);
            Assert.Equal(new LocalDate(2023, 7, 1), flights[0].DepartureDate);

            Assert.Equal(2, flights[1].Id);
            Assert.Equal("Oceanic Airlines", flights[1].Airline);
            Assert.Equal("MAN", flights[1].From);
            Assert.Equal("AGP", flights[1].To);
            Assert.Equal(245, flights[1].Price);
            Assert.Equal(new LocalDate(2023, 7, 1), flights[1].DepartureDate);
        }
    }
}
