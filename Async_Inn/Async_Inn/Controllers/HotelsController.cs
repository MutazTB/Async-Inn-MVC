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
    public class HotelsController : ControllerBase
    {
        private readonly IHotels _hotels;

        public HotelsController(IHotels hotels)
        {
            _hotels = hotels;
        }

        // GET: api/Hotels
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            return await _hotels.GetHotels();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotels.GetHotel(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            var modifiedHotel = await _hotels.UpdateHotel(id, hotel);

            return Ok(modifiedHotel);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
           var Addedhotels = await _hotels.Create(hotel);            

            return Ok(Addedhotels);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotels.Delete(id);

            return NoContent();
        }

        //private bool HotelExists(int id)
        //{
        //    return _context.Hotels.Any(e => e.Id == id);
        //}
    }
}
