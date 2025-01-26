using OnTheBeach.Models;

namespace OnTheBeach.Repositories
{
    public class HotelRepository
    {
        private readonly List<Hotel> _hotels;

        public HotelRepository(List<Hotel> hotels)
        {
            _hotels = hotels ?? throw new ArgumentNullException(nameof(hotels));
        }

        public List<Hotel> GetAllHotels()
        {
            return _hotels;
        }
    }
}
