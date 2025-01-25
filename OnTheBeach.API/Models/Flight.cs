using System.Text.Json.Serialization;
using NodaTime;

namespace OnTheBeach.Models
{
    public class Flight
    {
        [JsonPropertyName("id")]
        public int Id { get; }

        [JsonPropertyName("airline")]
        public string Airline { get; }

        [JsonPropertyName("from")]
        public string From { get; }

        [JsonPropertyName("to")]
        public string To { get; }

        [JsonPropertyName("price")]
        public int Price { get; }

        [JsonPropertyName("departure_date")]
        public LocalDate DepartureDate { get; }

        [JsonConstructor]
        public Flight(int id, string airline, string from, string to, int price, LocalDate departureDate)
        {
            Id = id;
            Airline = airline;
            From = from;
            To = to;
            Price = price;
            DepartureDate = departureDate;
        }
    }
}
