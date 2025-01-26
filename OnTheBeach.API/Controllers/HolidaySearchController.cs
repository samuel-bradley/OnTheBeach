using Microsoft.AspNetCore.Mvc;
using OnTheBeach.Models;
using OnTheBeach.API.Services;
using OnTheBeach.API.Models;

namespace OnTheBeach.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly HolidayService _holidayService;

        public HolidaysController(HolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        // POST api/holidays/search
        [HttpPost("search")]
        public ActionResult<List<Holiday>> SearchHolidays([FromBody] HolidaySearch search)
        {
            if (search == null)
            {
                return BadRequest("Search parameters cannot be null.");
            }

            var holidays = _holidayService.FindHolidays(search);

            if (holidays == null || holidays.Count == 0)
            {
                return NotFound("No holidays found matching the search criteria.");
            }

            return Ok(holidays);
        }
    }
}
