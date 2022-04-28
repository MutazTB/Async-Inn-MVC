using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IAminities
    {
        public Task<Amenities> Create(AmenityDTO amenities);

        public Task<List<AmenityDTO>> GetAmenitiess();

        public Task<AmenityDTO> GetAmenities(int id);

        public Task<Amenities> UpdateAmenities(int id, Amenities amenities);

        public Task Delete(int id);
    }
}
