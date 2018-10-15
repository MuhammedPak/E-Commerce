using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain
{
    public class Image:BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
