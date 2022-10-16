using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarRentalApi.Models;

namespace CarRentalApi.Data
{
    public class CarRentalApiContext : DbContext
    {
        public CarRentalApiContext (DbContextOptions<CarRentalApiContext> options)
            : base(options)
        {
        }

        public DbSet<CarRentalApi.Models.Car> Car { get; set; } = default!;

        public DbSet<CarRentalApi.Models.Color> Color { get; set; }

        public DbSet<CarRentalApi.Models.FuelType> FuelType { get; set; }

        public DbSet<CarRentalApi.Models.Location> Location { get; set; }
    }
}
