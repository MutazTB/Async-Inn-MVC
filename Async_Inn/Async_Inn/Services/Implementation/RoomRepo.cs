using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Implementation
{
    public class RoomRepo : IRooms
    {
        private readonly AsyncInnDbContext _context;

        public RoomRepo(AsyncInnDbContext context)
        {
            _context = context;
        }       

        public async Task<Room> Create(RoomDTO roomdto)
        {

            Room room = new Room()
            {
                Id = roomdto.ID,
                Name = roomdto.Name,
                Layout = roomdto.Layout
            };
        _context.Entry(room).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            //Room room = await _context.Rooms.FindAsync(id);

            //var roomAmenities = await _context.RoomAmenities
            //    .Where(ra => ra.RoomId == id)
            //    .Include(a => a.Amenities)
            //    .ToListAsync();

            //room.Amenities = roomAmenities;

            //return room;
            return await _context.Rooms
                .Select(room => new RoomDTO
                {
                    ID = room.Id,
                    Name = room.Name,
                    Layout = room.Layout,
                    Amenities = room.Amenities
                        .Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenities.Id,
                            Name = ra.Amenities.Name
                        }).ToList()
                }).FirstOrDefaultAsync(r => r.ID == id);

        }

        public async Task<List<RoomDTO>> GetRooms()
        {
           
            //var rooms = await _context.Rooms.Include(x => x.Amenities)
            //                               .ThenInclude(x => x.Amenities)
            //                               .ToListAsync();

            //List<Room> roomList = new List<Room>();

            //foreach (var room in rooms)
            //    roomList.Add(await GetRoom(room.Id));

            //return roomList;
            return await _context.Rooms
                .Select(room => new RoomDTO
                {
                    ID = room.Id,
                    Name = room.Name,
                    Layout = room.Layout,
                    Amenities = room.Amenities
                        .Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenities.Id,
                            Name = ra.Amenities.Name
                        }).ToList()
                }).ToListAsync();
        }
 
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenities roomAmenities = new RoomAmenities()
            {
                AmenitiesId = amenityId,
                RoomId = roomId
            };

            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            RoomAmenities roomAmenities = await _context.RoomAmenities.Where(x => x.AmenitiesId == amenityId).FirstOrDefaultAsync();

            _context.Entry(roomAmenities).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }       

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
    }
}






//public async Task<Room> GetRoom(int id)
//{
//    // get the room
//    Room room = await _context.Rooms.FindAsync(id);

//    // get the room amenities
//    var roomAmenities = await _context.RoomAmenities
//        .Where(ra => ra.RoomId == id)
//        .Include(a => a.Amenity)
//        .ToListAsync();

//    // assign amenities to the got room
//    room.RoomAmenity = roomAmenities;

//    return room;











