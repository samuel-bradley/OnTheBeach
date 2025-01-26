using NodaTime;
using OnTheBeach.API.Services;
using OnTheBeach.Models;
using OnTheBeach.Repositories;

namespace OnTheBeach.API.Tests
{
    public class HolidayServiceTests
    {
        private readonly FlightRepository _flightRepository;
        private readonly HotelRepository _hotelRepository;
        private readonly HolidayService _holidayService;

        public HolidayServiceTests()
        {
            List<Flight> flights = [
                new Flight(1, "Airline1", "London", "Madrid", 200, new LocalDate(2025, 6, 15)),
                new Flight(2, "Airline2", "London", "Madrid", 250, new LocalDate(2025, 6, 15))
            ];
            List<Hotel> hotels = [
                new Hotel(1, "Hotel1", new LocalDate(2025, 6, 15), 100, ["Madrid"], 6),
                new Hotel(2, "Hotel2", new LocalDate(2025, 6, 15), 120, ["Madrid"], 6),
                new Hotel(3, "Hotel3", new LocalDate(2025, 6, 15), 150, ["Barcelona"], 6)
            ];
            _flightRepository = new FlightRepository(flights);
            _hotelRepository = new HotelRepository(hotels);
            _holidayService = new HolidayService(_flightRepository, _hotelRepository);
        }

        [Fact]
        public void FindHolidays_WhenMatchingFlightsAndHotelsExist_ReturnsExpectedHolidays()
        {
            var search = new HolidaySearch("London", "Madrid", new LocalDate(2025, 6, 15), 7);

            var results = _holidayService.FindHolidays(search);

            Assert.Equal(4, results.Count);
            Assert.Contains(results, h => h.Flight.Airline == "Airline1" && h.Hotel.Name == "Hotel1");
            Assert.Contains(results, h => h.Flight.Airline == "Airline1" && h.Hotel.Name == "Hotel2");
            Assert.Contains(results, h => h.Flight.Airline == "Airline2" && h.Hotel.Name == "Hotel1");
            Assert.Contains(results, h => h.Flight.Airline == "Airline2" && h.Hotel.Name == "Hotel2");
        }

        [Fact]
        public void FindHolidays_WhenMatchingFlightsAndHotelsExist_ReturnsHolidaysSortedTotalPriceAscending()
        {
            var search = new HolidaySearch("London", "Madrid", new LocalDate(2025, 6, 15), 7);

            var results = _holidayService.FindHolidays(search);

            Assert.Equal(4, results.Count);
            Assert.Equal(800, results[0].Price());
            Assert.Equal(850, results[1].Price());
            Assert.Equal(920, results[2].Price());
            Assert.Equal(970, results[3].Price());
        }

        [Fact]
        public void FindHolidays_WhenNoMatchingFlightExists_ReturnsEmptyList()
        {
            var search = new HolidaySearch("Paris", "Madrid", new LocalDate(2025, 6, 15), 7);

            var results = _holidayService.FindHolidays(search);

            Assert.Empty(results);
        }

        [Fact]
        public void FindHolidays_WhenNoMatchingHotelExists_ReturnsEmptyList()
        {
            var search = new HolidaySearch("London", "Barcelona", new LocalDate(2025, 6, 15), 7);

            var results = _holidayService.FindHolidays(search);

            Assert.Empty(results);
        }

        [Fact]
        public void FindHolidays_WhenFlightAndHotelMatchDifferentDestinations_ReturnsEmptyList()
        {
            var search = new HolidaySearch("London", "Barcelona", new LocalDate(2025, 6, 15), 7);

            var results = _holidayService.FindHolidays(search);

            Assert.Empty(results);
        }
    }
}
