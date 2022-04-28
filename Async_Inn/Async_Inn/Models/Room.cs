using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Layout { get; set; }

        public IList<RoomAmenities> Amenities { get; set; }
        public IList<HotelRoom> Rooms { get; set; }
        
    }
}
