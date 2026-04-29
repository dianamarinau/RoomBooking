namespace RoomBooking.API.DTOs
{
    public class RegisterUserDto
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
