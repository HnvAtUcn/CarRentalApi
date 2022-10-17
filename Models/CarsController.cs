using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //string connString = ConfigurationManager.ConnectionStrings["DapperConnStr"].ConnectionString;
        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            //return await _context.Car.ToListAsync();
            return NoContent();
        }
    }
}
