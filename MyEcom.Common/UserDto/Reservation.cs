using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Common.UserDto
{
   public class ReservationDto
    {
        public int otelid { get; set; }
        public int roomid { get; set; }
        public string name { get; set; }
        public string email  { get; set; }
        public string password { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
    }
}
