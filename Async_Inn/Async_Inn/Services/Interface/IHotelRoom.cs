using Async_Inn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IHotelRoom
    {
        // CRUD
        public Task<HotelRoom> Create(int hotelId, HotelRoom hotelRoom);
        
        public Task Delete(int hotelId, int roomNumber);

        public Task<List<HotelRoom>> GetHotelRooms(int hotelId);

        public Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);
        
        public Task UpdateHotelRoom(HotelRoom hotelRoom);-
    }
}
