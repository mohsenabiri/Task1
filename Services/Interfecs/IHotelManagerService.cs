using Models.Alternative;
using Models.AlternativeHotel;
using Models.Availability;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfecs
{
    public interface IHotelManagerService
    {
        public List<Hotel> AlternativeHotel();
        public RoomTable GetAvailability();
        public string GetHodelDescription();
        public List<string> GetHotelName();
        public List<Link> GetAlternativeLink();
    }
}
