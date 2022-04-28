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
    public class AmenitiesRepo : IAminities
    {
        private readonly AsyncInnDbContext _context;

        public AmenitiesRepo(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Amenities> Create(AmenityDTO amenityDTO)
        {
            //_context.Entry(amenities).State = EntityState.Added;
            //await _context.SaveChangesAsync();
            //return amenities;
            Amenities amenity = new Amenities()
            {
                Id = amenityDTO.ID,
                Name = amenityDTO.Name
            };
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenities amenities = await _context.Amenities.FindAsync(id);
            _context.Entry(amenities).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<AmenityDTO> GetAmenities(int id)
        {
            //Amenities amenities = await _context.Amenities.FindAsync(id);
            //return amenities;
            return await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    ID = amenity.Id,
                    Name = amenity.Name
                }).FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<List<AmenityDTO>> GetAmenitiess()
        {
            //var amenitiess = await _context.Amenities.ToListAsync();
            //return amenitiess;
            return await _context.Amenities
               .Select(amenity => new AmenityDTO
               {
                   ID = amenity.Id,
                   Name = amenity.Name
               }).ToListAsync();
        }

        public async Task<Amenities> UpdateAmenities(int id, Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenities;
        }
    }
}
