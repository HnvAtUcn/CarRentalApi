using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace CarRentalApi.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        string connString = "Data Source=HENRIK-DESKTOP;Integrated Security=True;Initial Catalog=CarRental-V1";
        string sql1 = "SELECT * FROM Car";
        string sql2 = "SELECT * FROM Car WHERE Id = ";
        string sql3 = "UPDATE Car SET Brand = @Brand, FuelTypeId = @FuelTypeId, PassengerCapacity = @PassengerCapacity, " +
            "KilometersDriven = @KilometersDriven, LocationId = @LocationId, ProductionYear = @ProductionYear, " +
            "LicenseNo = @LicenseNo, ColorId = @ColorId WHERE Id = @Id";


        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            using (SqlConnection DapperObj = new SqlConnection(connString))
            {
                return DapperObj.Query<Car>(sql1).ToList();
            }

        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            using (SqlConnection DapperObj = new SqlConnection(connString))
            {
                return DapperObj.Query<Car>(sql2 + id).First();
            }
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            using (SqlConnection DapperObj = new SqlConnection(connString))
            {
                Car? carToMod = (Car)DapperObj.Query<Car>(sql2 + id).First();

                if (carToMod != null)
                {
                    // This should be hidden away in some DAL
                    carToMod = car;
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id, DbType.Int32);
                    parameters.Add("Brand", car.Brand, DbType.String);
                    parameters.Add("FuelTypeId", car.FuelTypeId, DbType.Int32);
                    parameters.Add("PassengerCapacity", car.PassengerCapacity, DbType.Int32);
                    parameters.Add("KilometersDriven", car.KilometersDriven, DbType.Int32);
                    parameters.Add("LocationId", car.LocationId, DbType.Int32);
                    parameters.Add("ProductionYear", car.ProductionYear, DbType.Int32);
                    parameters.Add("LicenseNo", car.LicenseNo, DbType.String);
                    parameters.Add("ColorId", car.ColorId, DbType.Int32);
                    var affectedRows = DapperObj.Execute(sql3, parameters);
                }
            }

            return NoContent();
        }


    }
}
