using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBooking.API.DTOs;
using RoomBooking.API.Models;

namespace RoomBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomBookingContext _context;

        public RoomsController(RoomBookingContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            return await _context.Rooms.Select(r => new RoomDto
            {
                RoomId = r.RoomId,
                RoomName = r.RoomName,
                Capacity = r.Capacity,
                HasProjector = r.HasProjector,
                IsAvailable = r.IsAvailable
            }).ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            return new RoomDto
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                Capacity = room.Capacity,
                HasProjector = room.HasProjector,
                IsAvailable = room.IsAvailable
            };
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDto dto)
        {
            var validationErrors = dto.Validate();

            if (validationErrors.Any())
                return BadRequest(new { Errors = validationErrors });

            if (id != dto.RoomId)   return BadRequest("ID mismatched");

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)   return NotFound();

            room.RoomName = dto.RoomName;
            room.Capacity = dto.Capacity;
            room.HasProjector = dto.HasProjector;
            room.IsAvailable = dto.IsAvailable;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))    return NotFound();
                else   throw;
            }

            return NoContent();
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<RoomDto>> PostRoom(RoomDto dto)
        {
            var validationErrors = dto.Validate();

            if(validationErrors.Any())
                return BadRequest(new { Errors = validationErrors });

            var room = new Room
            {
                RoomName = dto.RoomName,
                Capacity = dto.Capacity,
                HasProjector = dto.HasProjector,
                IsAvailable = dto.IsAvailable
            };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            dto.RoomId = room.RoomId;  // Update DTO with generated ID

            return CreatedAtAction(nameof(GetRoom), new { id = dto.RoomId }, dto);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
