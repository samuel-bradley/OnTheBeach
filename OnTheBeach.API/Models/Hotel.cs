using System.Text.Json.Serialization;
using NodaTime;

namespace OnTheBeach.Models
{
    public class Hotel
    {
        [JsonPropertyName("id")]
        public int Id { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("arrival_date")]
        public LocalDate ArrivalDate { get; }

        [JsonPropertyName("price_per_night")]
        public int PricePerNight { get; }

        [JsonPropertyName("local_airports")]
        public IReadOnlyList<string> LocalAirports { get; }

        [JsonPropertyName("nights")]
        public int Nights { get; }

        [JsonConstructor]
        public Hotel(int id, string name, LocalDate arrivalDate, int pricePerNight, IReadOnlyList<string> localAirports, int nights)
        {
            Id = id;
            Name = name;
            ArrivalDate = arrivalDate;
            PricePerNight = pricePerNight;
            LocalAirports = localAirports;
            Nights = nights;
        }
    }
}
