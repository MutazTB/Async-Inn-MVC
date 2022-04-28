﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class HotelRoom
    {       
                
        public Hotel Hotel { get; set; }

        public int HotelId { get; set; }

        public int RoomNumber { get; set; }
       
        public Room Room { get; set; }

        public int RoomId { get; set; }

        public decimal Rate { get; set; }

        public bool PetFriendly { get; set; }


    }
}
