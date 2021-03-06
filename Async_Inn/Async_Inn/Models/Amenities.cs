using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class Amenities
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<RoomAmenities> Amenity { get; set; }
    }
}
