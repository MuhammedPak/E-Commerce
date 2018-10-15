using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain
{
    public class Reservation : BaseEntity<int>
    {
        public int hotel_id { get; set; }
        public int customerid { get; set; }
        public int roomid { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
    }
}
