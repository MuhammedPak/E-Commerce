using MyEcom.Common.RoomDto;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Common.RedisDto
{
    public class RedisDto
    {
        public HotelContent HotelContent { get; set; }
        public Hotel Hotel { get; set; }
        public  IEnumerable<Room> Room { get; set; }
      
    }
    public class UserDto
    {
        public RedisDto RedisDto { get; set; }
        public IEnumerable<HotelImageList> Images { get; set; }

    }
    public class UserHomePageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public bool Spa { get; set; }
        public bool Restaurant { get; set; }
        public bool Gym { get; set; }
        public bool RoomService { get; set; }
        public bool Pool { get; set; }
        public IEnumerable<HotelImageList> Images { get; set; }
        public IEnumerable<RoomDto.RoomDto> Room { get; set; }

    }
    public class ElasticDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    }
