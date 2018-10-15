using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain
{
    public class HotelContent:BaseEntity<int>
    {
        public virtual Hotel Hotel{ get; set; }
        public int HotelId { get; set; }
        public bool Spa { get; set; }
        public bool Restaurant { get; set; }
        public bool Gym { get; set; }
        public bool RoomService { get; set; }
        public bool Pool { get; set; }
        public int Rank { get; set; }
    }
}
