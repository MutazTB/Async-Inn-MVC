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
    public class RoomsController : ControllerBase
    {
        private readonly IRooms _rooms;

        public RoomsController(IRooms rooms)
        {
            _rooms = rooms;
        }

        // GET: api/Rooms
        [Authorize(Roles = "Property Manager")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _rooms.GetRooms();
        }

        // GET: api/Rooms/5
        [Authorize(Roles = "Property Manager")]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            var room = await _rooms.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Property Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var modifiedRoom = await _rooms.UpdateRoom(id, room);

            return Ok(modifiedRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomDTO room)
        {
            var AddedRoom = await _rooms.Create(room);

            return Ok(AddedRoom);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _rooms.Delete(id);

            return NoContent();
        }
        [Authorize(Roles = "Property Manager")]
        [Authorize(Roles = "Agent")]
        [HttpPost]
        [Route("{roomId}/{amenityId}")]
        public async Task<ActionResult<Room>> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _rooms.AddAmenityToRoom(roomId, amenityId);
            return NoContent();
        }        
        [Authorize(Roles = "Agent")]
        [HttpDelete]
        [Route("{roomId}/{amenityId}")]
        public async Task<ActionResult<Room>> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var amenityRoom = await _rooms.GetRoom(roomId);
            if (amenityRoom == null)
            {
                return NotFound();
            }

            await _rooms.RemoveAmentityFromRoom(roomId, amenityId);

             await _rooms.GetRoom(roomId);

            return NoContent();
        }
        //private bool RoomExists(int id)
        //{
        //    return _context.Rooms.Any(e => e.Id == id);
        //}
    }
}
