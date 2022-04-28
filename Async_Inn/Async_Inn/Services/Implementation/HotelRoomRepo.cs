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
    public class HotelRoomRepo : IHotelRoom
    {

        private readonly AsyncInnDbContext _context;
        private readonly IRooms _rooms;

        public HotelRoomRepo(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<HotelRoom> Create(int hotelId, HotelRoom hotel)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                HotelId = hotelId,
                RoomNumber = hotel.RoomNumber,
                RoomId = hotel.RoomId,
                Rate = hotel.Rate,
                PetFriendly = hotel.PetFriendly,
            };

            _context.Entry(hotelRoom).State = EntityState.Added;

            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRoom.FindAsync(hotelId, roomNumber);

            _context.Entry(hotelRoom).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            //var hotelRooms = await _context.HotelRoom.Where(x => x.HotelId == hotelId)
            //                                           .Include(x => x.Room)
            //                                           .ThenInclude(x => x.Rooms)
            //                                           .ToListAsync();

            //List<HotelRoom> hotelRoomList = new List<HotelRoom>();

            //foreach (var hotelRoom in hotelRooms)
            //    hotelRoomList.Add(await GetHotelRoom(hotelRoom.HotelId, hotelRoom.RoomNumber));

            //return hotelRoomList;
            return await _context.HotelRoom
               .Select(hotelRooms => new HotelRoomDTO
               {
                   HotelID = hotelRooms.HotelId,
                   RoomID = hotelRooms.RoomId,
                   Room = new RoomDTO
                   {
                       ID = hotelRooms.Room.Id,
                       Name = hotelRooms.Room.Name,
                       Layout = hotelRooms.Room.Layout
                   }
               }).ToListAsync();
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            //var hotelRoom = await _context.HotelRoom.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
            //                                        .Include(x => x.Room)
            //                                        .ThenInclude(x => x.Amenities)
            //                                        .FirstOrDefaultAsync();

            //HotelRoom hotel_Room = new HotelRoom()
            //{
            //    HotelId = hotelRoom.HotelId,
            //    RoomNumber = hotelRoom.RoomNumber,
            //    Rate = hotelRoom.Rate,
            //    PetFriendly = hotelRoom.PetFriendly,
            //    RoomId = hotelRoom.RoomId,
            //    Room = await _rooms.GetRoom(hotelRoom.RoomId)
            //};

            //return hotel_Room;
            return await _context.HotelRoom.Select(hotelRoom => new HotelRoomDTO()
            {
                HotelID = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomID = hotelRoom.RoomId,
                Room = new RoomDTO()
                {
                    ID = hotelRoom.Room.Id,
                    Name = hotelRoom.Room.Name,
                    Layout = hotelRoom.Room.Layout,
                    Amenities = hotelRoom.Room.Amenities
                                 .Select(roomAmenity => new AmenityDTO()
                                 {
                                 ID = roomAmenity.Amenities.Id,
                                 Name = roomAmenity.Amenities.Name
                                 })
                                 .ToList()
                }
            }).FirstOrDefaultAsync(x => x.HotelID == hotelId && x.RoomNumber == roomNumber);
        }

        public async Task UpdateHotelRoom(HotelRoom hotelRoom)
        {
            var hotel_Room = await _context.HotelRoom.Where(x => x.HotelId == hotelRoom.HotelId && x.RoomNumber == hotelRoom.RoomNumber)
                                         .Include(x => x.Room)
                                         .ThenInclude(x => x.Amenities)
                                         .FirstOrDefaultAsync();

            hotel_Room.RoomId = hotelRoom.RoomId;
            hotel_Room.Rate = hotelRoom.Rate;
            hotel_Room.PetFriendly = hotelRoom.PetFriendly;

            _context.Entry(hotelRoom).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
