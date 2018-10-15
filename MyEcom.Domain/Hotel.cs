using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain.Abstract
{
    public class Hotel:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Room> Room { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}
