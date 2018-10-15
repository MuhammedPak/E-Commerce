using MyEcom.Common.RoomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Common.UserDto
{
    public class SingleRoomDto
    {
        public string HotelName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int RoomId { get; set; }
        public int Otel_id { get; set; }
        public int Price { get; set; }
        public int Bed { get; set; }
        public string RoomType { get; set; }
        public bool Wifi { get; set; }
        public bool TV { get; set; }
        public bool Bathroom { get; set; }
        public bool Aircon { get; set; }
        public bool Jacuzzi { get; set; }
        public bool MiniBar { get; set; }
        public string RoomImage { get; set; }

        public List<HotelImageList> ImageList { get; set; }

    }
}
