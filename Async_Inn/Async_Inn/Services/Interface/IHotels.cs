using Async_Inn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IHotels
    { // CRUD
        public Task<Hotel> Create(Hotel hotel);

        public Task<List<Hotel>> GetHotels();

        public Task<Hotel> GetHotel(int id);

        public Task<Hotel> UpdateHotel(int id ,Hotel hotel);

        public Task Delete(int id);


    }
}
