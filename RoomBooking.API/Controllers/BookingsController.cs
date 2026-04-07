using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBooking.API.DTOs;
using RoomBooking.API.Models;

namespace RoomBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly RoomBookingContext _context;

        public BookingsController(RoomBookingContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookings()
        {
            return await _context.Bookings.Include(b => b.Room).Include(b => b.User).Select(b => new BookingResponseDto
            {
                BookingId = b.BookingId,
                RoomId = b.RoomId,
                RoomName = b.Room!.RoomName,
                UserFullName = b.User!.FullName,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Status = b.Status
            })
            .ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponseDto>> GetBooking(int id)
        {
            var booking = await _context.Bookings.Include(b => b.Room).Include(b => b.User).FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return new BookingResponseDto
            {
                BookingId = booking.BookingId,
                RoomId = booking.RoomId,
                RoomName= booking.Room!.RoomName,
                UserFullName= booking.User!.FullName,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Status = booking.Status
            };
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, CreateBookingDto dto)
        { 
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            //Validation
            var validationErrors = dto.Validate().ToList();
            if (validationErrors.Any())
                return BadRequest(validationErrors.Select(e => e.ErrorMessage));

            //Overlap check
            var isBooked = await _context.Bookings.AnyAsync(b =>
                b.BookingId != id &&
                b.RoomId == dto.RoomId &&
                b.StartTime < dto.EndTime &&
                dto.StartTime < b.EndTime);

            if (isBooked)
                return BadRequest("This room is already booked for the selected time");

            booking.RoomId = dto.RoomId;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(CreateBookingDto dto)
        {
            //Validation
            var validationErrors = dto.Validate().ToList();
            if (validationErrors.Any())
                return BadRequest(validationErrors.Select(e => e.ErrorMessage));

            //Overlap check
            var isBooked = await _context.Bookings.AnyAsync(b => 
                b.RoomId == dto.RoomId &&
                b.StartTime < dto.EndTime &&
                dto.StartTime < b.EndTime);

            if (isBooked)
                return BadRequest("This room is already booked for the selected time");

            var booking = new Booking
            {
                RoomId = dto.RoomId,
                UserId = 1,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = "Confirmed"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            await _context.Entry(booking).Reference(b => b.Room).LoadAsync();
            await _context.Entry(booking).Reference(b => b.User).LoadAsync();

            var response = new BookingResponseDto
            {
                BookingId = booking.BookingId,
                RoomId = booking.RoomId,
                RoomName = booking.Room!.RoomName,
                UserFullName = booking.User!.FullName,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Status = booking.Status
            };

            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, response);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
