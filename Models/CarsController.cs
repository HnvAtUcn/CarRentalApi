using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalApi.Data;

namespace CarRentalApi.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRentalApiContext _context;

        public CarsController(CarRentalApiContext context)
        {
            _context = context;
        }

        //// GET: api/Cars
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        //{
        //    return await _context.Car.ToListAsync();
        //}

        // GET: api/Cars?rented
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar([FromQuery]bool? rented)
        {
            //return await _context.Car.ToListAsync();
            var CarList = await _context.Car.ToListAsync();

            if (rented == null)
            {
                return CarList;
            }

            var CarsRentedOrNot = new List<Car>();
            bool want_rented = (bool)rented;
            foreach (Car theCar in CarList)
            {
                if (want_rented)
                {
                    if (theCar.LocationId == null)
                    {
                        CarsRentedOrNot.Add(theCar);
                    }
                }
                else
                {
                    if (theCar.LocationId != null)
                    {
                        CarsRentedOrNot.Add(theCar);
                    }
                }
            }
            return CarsRentedOrNot;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
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

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Cars/5/carrental
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/carrental")]
        public async Task<IActionResult> PutCarRental(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            var CarList = await _context.Car.ToListAsync();
            Car? moddedCar = CarList.Find(x => x.Id == id);

            if (moddedCar == null)
            {
                return BadRequest();
            }

            if (moddedCar.LocationId == null)
            {
                return BadRequest("The car is already rented you twit!");
            }

            moddedCar.LocationId = null;
            car = moddedCar;

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //PUT: api/Cars/5/Returnal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/carreturnal/{locationId}")]
        public async Task<IActionResult> PutCarReturnal(int id, Car car, int locationId)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            var CarList = await _context.Car.ToListAsync();
            Car? moddedCar = CarList.Find(x => x.Id == id);

            if (moddedCar == null)
            {
                return BadRequest();
            }

            if (moddedCar.LocationId != null)
            {
                return BadRequest("The car cannot be returned since it ain't rented you clown!");
            }

            moddedCar.LocationId = locationId;
            car = moddedCar;

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}
