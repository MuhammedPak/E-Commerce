using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain
{
    public class Room : BaseEntity<int>
    {
        public virtual Hotel Hotel { get; set; }      
        public int HotelId { get; set; }


        public int Stock { get; set; }
        public int Price { get; set; }
        public string RoomType { get; set; }

        public virtual RoomContent RoomContent { get; set; }
        public int RoomContentId { get; set; }


    }
}
