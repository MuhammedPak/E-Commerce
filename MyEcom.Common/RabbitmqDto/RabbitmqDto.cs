using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Common.RabbitmqDto
{
    public class RabbitmqDto
    {
        public int otelid { get; set; }
        public int roomid { get; set; }
        public int customerid { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
    }
    public class ReservationViewDto
    {
        public int roomid { get; set;}
        public string Name { get; set;}
        public string  RoomImage { get; set;}
        public string Phone { get; set;}
        public string Email{ get; set;}
        public DateTime checkin { get; set;}
        public DateTime checkout { get; set;}


    }

}
