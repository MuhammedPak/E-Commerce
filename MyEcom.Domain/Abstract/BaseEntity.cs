using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain.Abstract
{
    public abstract class BaseEntity<TprimaryKey> : IBaseEntity<TprimaryKey>
    {
        public TprimaryKey Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
