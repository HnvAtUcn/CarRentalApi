using System;
using System.Collections.Generic;

namespace CarRentalApi.Models
{
    public partial class Location
    {
        public Location()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
