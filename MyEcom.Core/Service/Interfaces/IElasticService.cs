using MyEcom.Common.RedisDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service.Interfaces
{
    public interface IElasticService
    {
        void CreateIndex();
        List<ElasticDto> Search(string key);
    }
}
