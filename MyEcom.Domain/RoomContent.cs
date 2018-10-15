using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain
{
    public class RoomContent:BaseEntity<int>
    {   public virtual Image Image { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
        public int PersonCount { get; set; }
        public bool Wifi { get; set; }
        public bool TV { get; set; }
        public bool Bathroom { get; set; }
        public bool Aircon { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Minibar { get; set; }

    }
}
