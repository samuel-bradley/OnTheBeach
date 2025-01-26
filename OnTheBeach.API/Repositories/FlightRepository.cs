using OnTheBeach.Models;

namespace OnTheBeach.Repositories
{
    public class FlightRepository
    {
        private readonly List<Flight> _flights;

        public FlightRepository(List<Flight> flights)
        {
            _flights = flights ?? throw new ArgumentNullException(nameof(flights));
        }

        public List<Flight> GetAllFlights()
        {
            return _flights;
        }
    }
}
