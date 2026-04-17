namespace RoomBooking.API.DTOs
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public int Capacity { get; set; }
        public bool? HasProjector { get; set; }
        public bool? IsAvailable { get; set; }

        public List<string> Validate()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(RoomName))
                errors.Add("Room name is requiered.");

            if (Capacity <= 0)
                errors.Add("Room capacity must be greater than 0.");

            return errors;
        }
    }
}
