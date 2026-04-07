namespace RoomBooking.API.DTOs
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public int Capacity { get; set; }
        public bool? HasProjector { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
