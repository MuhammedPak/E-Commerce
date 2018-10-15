using MyEcom.Common.RoomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service.Interfaces
{
    public interface IGalleryService
    {
        void AddImage(string name);
        void AddHotelImage(string name);
        void DeleteRoom(int id);
        IEnumerable<HotelImageList> GetHotelImageList();
        void DeleteHotelImage(int id);


    }
}
