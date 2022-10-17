using System;
using System.Collections.Generic;

namespace CarRentalApi.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public int FuelTypeId { get; set; }
        public int? PassengerCapacity { get; set; }
        public int? KilometersDriven { get; set; }
        public int? LocationId { get; set; }
        public int? ProductionYear { get; set; }
        public string? LicenseNo { get; set; }
        public int ColorId { get; set; }

        //public virtual Color Color { get; set; } = null!;
        //public virtual FuelType FuelType { get; set; } = null!;
        //public virtual Location? Location { get; set; }
    }
}
