using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Services.Interface;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Authorize(Roles = "District Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAminities _aminities;

        public AmenitiesController(IAminities aminities)
        {
            _aminities = aminities;
        }

        // GET: api/Amenities        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            return await _aminities.GetAmenitiess();
        }

        // GET: api/Amenities/5        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AmenityDTO>> GetAmenities(int id)
        {
            var amenities = await _aminities.GetAmenities(id);

            if (amenities == null)
            {
                return NotFound();
            }

            return amenities;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, Amenities amenities)
        {
            if (id != amenities.Id)
            {
                return BadRequest();
            }

            var modifiedAmenitis = await _aminities.UpdateAmenities(id, amenities);

            return Ok(modifiedAmenitis);
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        public async Task<ActionResult<Amenities>> PostAmenities(AmenityDTO amenities)
        {
            var AddedAmenities = await _aminities.Create(amenities);

            return Ok(AddedAmenities);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenities(int id)
        {
            await _aminities.Delete(id);

            return NoContent();
        }

        //private bool AmenitiesExists(int id)
        //{
        //    return _context.Amenities.Any(e => e.Id == id);
        //}
    }
}
