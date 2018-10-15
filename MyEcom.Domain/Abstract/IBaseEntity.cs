using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Domain.Abstract
{
    public interface IBaseEntity<TprimaryKey>
    {    [Key]
        TprimaryKey Id { get; set; }
        DateTime CreateTime { get; set; }
        bool IsDeleted { get; set; }
    }
}
