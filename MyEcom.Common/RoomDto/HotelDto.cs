using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;

namespace MyEcom.Common.RoomDto
{
    public class HotelDto
    {
        public HotelContent HotelContent { get; set; }
        public Hotel Hotel { get; set; }

    }
    public class HotelViewDto
    {

        public bool Spa { get; set; }
        public bool Restaurant { get; set; }
        public bool Gym { get; set; }
        public bool RoomService { get; set; }
        public bool Pool { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }



    }
    public class HotelImageList
    {
        public int otelid { get; set; }
        public string ImageName { get; set; }
        public int ImageId { get; set; }
        public bool Active { get; set; }

    }

    public class HotelRedisDto
    {
        public int id { get; set; }
        public bool Spa { get; set; }
        public bool Restaurant { get; set; }
        public bool Gym { get; set; }
        public bool RoomService { get; set; }
        public bool Pool { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public List<HotelImageList> ImageList { get; set; }

    }
}