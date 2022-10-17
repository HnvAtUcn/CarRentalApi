using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        string connString = "Data Source=HENRIK-DESKTOP;Integrated Security=True;Initial Catalog=CarRental-V1";
        string sql1 = "SELECT * FROM Car";


        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                var books = connection.Query<Car>(sql1).ToList();
            }

            //return await _context.Car.ToListAsync();
            return NoContent();
        }
    }
}
