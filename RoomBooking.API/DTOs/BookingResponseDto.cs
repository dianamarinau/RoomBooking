namespace RoomBooking.API.DTOs
{
    public class BookingResponseDto
    {
            public int BookingId { get; set; }
            public int RoomId { get; set; }
            public string RoomName { get; set; } = null!;
            public string UserFullName { get; set; } = null!;
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string? Status { get; set; }
    }
}
