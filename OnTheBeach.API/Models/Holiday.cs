using OnTheBeach.Models;

namespace OnTheBeach.API.Models
{

    public class Holiday
    {
        public Flight Flight { get; }
        public Hotel Hotel { get; }

        public Holiday(Flight flight, Hotel hotel)
        {
            Flight = flight;
            Hotel = hotel;
        }

        public int Price()
        {
            return Flight.Price + Hotel.PricePerNight * Hotel.Nights;
        }
    }
}
