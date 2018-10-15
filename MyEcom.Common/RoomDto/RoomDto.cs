using MyEcom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyEcom.Common.RoomDto
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public int Otel_id { get; set; }
        public int RoomNumber { get; set; }
        public int Price { get; set; }
        public int Bed { get; set; }
        public string RoomType { get; set; }
        public bool Wifi { get; set; }
        public bool Spa { get; set; }
        public bool TV { get; set; }
        public bool Bathroom { get; set; }
        public bool Aircon { get; set; }
        public bool Jacuzzi { get; set; }
        public bool MiniBar { get; set; }
        public string RoomImage { get; set; }


    }
    public class RoomList
    {
        public string RoomType { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public int RoomId { get; set; }
    }
    
  
       
    
}
