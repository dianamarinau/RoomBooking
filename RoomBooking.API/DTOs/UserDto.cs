namespace RoomBooking.API.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Role { get; set; } = "User";

        public List<string> Validate()
        {
            var errors = new List<string>();
            var validRoles = new[] { "Admin", "User", "Guest" };

            if (string.IsNullOrWhiteSpace(FullName))
                errors.Add("Name is required.");

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                errors.Add("A valid email is required.");

            if (!validRoles.Contains(Role))
                errors.Add($"Role must be one of: {string.Join(", ", validRoles)}");

            return errors;
        }
    }
}
