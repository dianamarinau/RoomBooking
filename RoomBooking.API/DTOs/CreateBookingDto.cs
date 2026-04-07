using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace RoomBooking.API.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (EndTime <= StartTime)
                yield return new ValidationResult("EndTime must be after StartTime");

            if(StartTime < DateTime.UtcNow)
                yield return new ValidationResult("Cannot book a room in the past.");
        }
    }
}
