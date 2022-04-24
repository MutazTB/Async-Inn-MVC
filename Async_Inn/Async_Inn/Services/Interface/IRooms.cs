using Async_Inn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IRooms
    {
        public Task<Room> Create(Room room);

        public Task<List<Room>> GetRooms();

        public Task<Room> GetRoom(int id);

        public Task<Room> UpdateRoom(int id, Room room);

        public Task Delete(int id);

        public Task<Room> AddAmenityToRoom(int roomId, int amenityId);

        public Task RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
