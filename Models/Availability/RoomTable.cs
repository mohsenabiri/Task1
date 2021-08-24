using System;
using System.Collections.Generic;
using System.Text;
using Models.Information;
namespace Models.Availability
{
    public class RoomTable :HotelInformation
    {
        public string Title { get; set; }
        public List<string> Header { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
