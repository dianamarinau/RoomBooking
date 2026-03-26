using System;
using System.Collections.Generic;

namespace RoomBooking.API.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Status { get; set; }

    public virtual Room ? Room { get; set; } = null!;

    public virtual User ? User { get; set; } = null!;
}
