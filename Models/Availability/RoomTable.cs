using System;
using System.Collections.Generic;
using System.Text;
namespace Models.Availability
{
    public class RoomTable 
    {
        public string Title { get; set; }
        public List<string> Header { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
