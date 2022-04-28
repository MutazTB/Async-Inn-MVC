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

namespace Async_Inn.Controllers
{
    [Route("api/Hotels")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        // GET: api/HotelRooms
        [HttpGet]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRoom(int hotelId)
        {
            return await _hotelRoom.GetHotelRooms(hotelId);
        }

        // GET: api/HotelRooms/5
        [HttpGet]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelRoom.GetHotelRoom(hotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, HotelRoom hotelRoom)
        {
            if (hotelId != hotelRoom.HotelId)
            {
                return BadRequest();
            }

            try
            {
                await _hotelRoom.UpdateHotelRoom(hotelRoom);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomExists(hotelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, HotelRoom hotelRoom)
        {
            var hotel_Room = await _hotelRoom.Create(hotelId, hotelRoom);

            return Ok(hotel_Room);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelRoom.GetHotelRoom(hotelId, roomNumber);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            await _hotelRoom.Delete(hotelId, roomNumber);

            return hotelRoom;
        }

        private bool HotelRoomExists(int hotelId)
        {
            return _hotelRoom.GetHotelRooms(hotelId).Result.Any(e => e.HotelID == hotelId);
        }
    }
}
