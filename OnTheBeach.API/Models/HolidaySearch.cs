using System.Text.Json.Serialization;
using NodaTime;

namespace OnTheBeach.Models
{
    public class HolidaySearch
    {
        [JsonPropertyName("departing_from")]
        public string DepartingFrom { get; }

        [JsonPropertyName("traveling_to")]
        public string TravelingTo { get; }

        [JsonPropertyName("departure_date")]
        public LocalDate DepartureDate { get; }

        [JsonPropertyName("duration")]
        public int Duration { get; }

        [JsonConstructor]
        public HolidaySearch(string departingFrom, string travelingTo, LocalDate departureDate, int duration)
        {
            DepartingFrom = departingFrom;
            TravelingTo = travelingTo;
            DepartureDate = departureDate;
            Duration = duration;
        }
    }
}
