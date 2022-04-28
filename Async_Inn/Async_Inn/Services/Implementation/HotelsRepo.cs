using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Services.Implementation
{
    public class HotelsRepo : IHotels
    {
        private readonly AsyncInnDbContext _context;

        public HotelsRepo(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelDTO> GetHotel(int id)
        {
            //Hotel hotel = await _context.Hotels.FindAsync(id);
            //return hotel;
            return await _context.Hotels.Select(x => new HotelDTO
            {
                ID = x.Id,
                Name = x.Name,
                StreetAddress = x.StreetAddress,
                City = x.City,
                State = x.State,
                Phone = x.Phone,
                Rooms = x.Rooms.Select(x => new HotelRoomDTO
                {
                    HotelID = x.HotelId,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly,
                    RoomID = x.RoomId,
                    Room = x.Room.Rooms.Select(x => new RoomDTO
                    {
                        ID = x.Room.Id,
                        Name = x.Room.Name,
                        Layout = x.Room.Layout,
                        Amenities = x.Room.Amenities.Select(x => new AmenityDTO
                        {
                            ID = x.Amenities.Id,
                            Name = x.Amenities.Name
                        }).ToList()
                    }).FirstOrDefault()
                }).ToList()
            }).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            //var hotels = await _context.Hotels.ToListAsync();
            //return hotels;

            return await _context.Hotels.Select(x => new HotelDTO
            {
                ID = x.Id,
                Name = x.Name,
                StreetAddress = x.StreetAddress,
                City = x.City,
                State = x.State,
                Phone = x.Phone,
                Rooms = x.Rooms.Select(x => new HotelRoomDTO
                {
                    HotelID = x.HotelId,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly,
                    RoomID = x.RoomId,
                    Room = x.Room.Rooms.Select(x => new RoomDTO
                    {
                        ID = x.Room.Id,
                        Name = x.Room.Name,
                        Layout = x.Room.Layout,
                        Amenities = x.Room.Amenities.Select(x => new AmenityDTO
                        {
                            ID = x.Amenities.Id,
                            Name = x.Amenities.Name
                        }).ToList()
                    }).FirstOrDefault()
                }).ToList()
            }).ToListAsync();
        }

        public async Task<Hotel> UpdateHotel(int id ,Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
