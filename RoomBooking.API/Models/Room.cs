using System;
using System.Collections.Generic;

namespace RoomBooking.API.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int Capacity { get; set; }

    public bool? HasProjector { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
