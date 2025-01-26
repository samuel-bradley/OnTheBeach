using OnTheBeach.API.Models;
using OnTheBeach.Models;
using OnTheBeach.Repositories;

namespace OnTheBeach.API.Services
{
    public class HolidayService(FlightRepository flightRepository, HotelRepository hotelRepository)
    {
        private readonly FlightRepository _flightRepository = flightRepository;
        private readonly HotelRepository _hotelRepository = hotelRepository;

        public List<Holiday> FindHolidays(HolidaySearch search)
        {
            // Filter flights and hotels to those matching the search
            List<Flight> matchingFlights = _flightRepository.GetAllFlights().FindAll((flight) => FlightMatchesSearch(flight, search));
            List<Hotel> matchingHotels = _hotelRepository.GetAllHotels().FindAll((hotel) => HotelMatchesSearch(hotel, search));

            // Build the list of possible holidays (flight-hotel combinations)
            List<Holiday> holidays = [];
            foreach (var flight in matchingFlights)
                foreach (var hotel in matchingHotels)
                    holidays.Add(new Holiday(flight, hotel));

            // Sort holidays in descending order of total price (flight price + hotel price)
            return [.. holidays.OrderBy(holiday => holiday.Price())];
        }

        private static bool FlightMatchesSearch(Flight flight, HolidaySearch search)
        {
            if (flight.DepartureDate != search.DepartureDate) return false;
            if (flight.From != search.DepartingFrom) return false;
            if (flight.To != search.TravelingTo) return false;
            return true;
        }

        private static bool HotelMatchesSearch(Hotel hotel, HolidaySearch search)
        {
            if (hotel.Nights != search.Duration - 1) return false; // Needs duration minus one night, since night of return date not used
            if (!hotel.ArrivalDate.Equals(search.DepartureDate)) return false; // Note this does not account for time zones and travel time
            if (!hotel.LocalAirports.Contains(search.TravelingTo)) return false;
            return true;
        }
    }
}
