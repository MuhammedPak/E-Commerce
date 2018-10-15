using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain
{
    public class Hotel_Image : BaseEntity<int>
    {
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
        public Image Image { get; set; }
        public int ImageId { get; set; }
        public bool  Active { get; set; }
    }

}
