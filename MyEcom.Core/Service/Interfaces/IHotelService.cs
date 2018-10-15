using MyEcom.Common.RoomDto;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyEcom.Core.Service.Interfaces
{
    public interface IHotelService
    {
        void AddHotel(Hotel hotel);
        void SetSession(string email, string password);
        void NewRoom(RoomDto r, string image);
        HotelDto GetHotelDto();
        void UpdateHotel(HotelViewDto hotelDto);

        IEnumerable<RoomList> RoomList();

        RoomDto GetRoom(int id);
        void UpdateRoom(RoomDto roomDto);

    }
}
