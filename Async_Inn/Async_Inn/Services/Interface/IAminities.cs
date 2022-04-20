using Async_Inn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IAminities
    {
        public Task<Amenities> Create(Amenities amenities);

        public Task<List<Amenities>> GetAmenitiess();

        public Task<Amenities> GetAmenities(int id);

        public Task<Amenities> UpdateAmenities(int id, Amenities amenities);

        public Task Delete(int id);
    }
}
