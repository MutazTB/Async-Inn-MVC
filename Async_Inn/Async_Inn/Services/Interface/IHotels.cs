using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IHotels
    { // CRUD
        public Task<Hotel> Create(Hotel hotel);

        public Task<List<HotelDTO>> GetHotels();

        public Task<HotelDTO> GetHotel(int id);

        public Task<Hotel> UpdateHotel(int id ,Hotel hotel);

        public Task Delete(int id);


    }
}
