using System;
using System.Collections.Generic;

namespace CarRentalApi.Models
{
    public partial class FuelType
    {
        public FuelType()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string FuelTypeName { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
