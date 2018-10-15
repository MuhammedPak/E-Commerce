using MyEcom.Common.RedisDto;
using MyEcom.Common.RoomDto;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service.Interfaces
{
    public interface IRedisService
    {
        void SaveHotel();
        RedisDto GetHotel(int id);
       
        void SaveImage();
        IEnumerable<HotelImageList> GetRedisHotelImageList();
        IEnumerable<RoomDto> GetRedisHotelRooms(int hotelid);
        List<UserHomePageDto> UserHomePage();
     

    }
}
