using Async_Inn.Data;
using Async_Inn.Models;
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

        public async Task<Amenities> Create(Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenities;
        }

        public async Task Delete(int id)
        {
            Amenities amenities = await GetAmenities(id);
            _context.Entry(amenities).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Amenities> GetAmenities(int id)
        {
            Amenities amenities = await _context.Amenities.FindAsync(id);
            return amenities;
        }

        public async Task<List<Amenities>> GetAmenitiess()
        {
            var amenitiess = await _context.Amenities.ToListAsync();
            return amenitiess;
        }

        public async Task<Amenities> UpdateAmenities(int id, Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenities;
        }
    }
}
