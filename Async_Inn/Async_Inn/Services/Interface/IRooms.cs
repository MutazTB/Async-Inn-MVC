using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IRooms
    {
        public Task<Room> Create(RoomDTO roomdto);

        public Task<List<RoomDTO>> GetRooms();

        public Task<RoomDTO> GetRoom(int id);

        public Task<Room> UpdateRoom(int id, Room room);

        public Task Delete(int id);

        public Task AddAmenityToRoom(int roomId, int amenityId);

        public Task RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
