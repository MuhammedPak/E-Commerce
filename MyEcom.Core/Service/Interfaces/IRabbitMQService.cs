using MyEcom.Common.RabbitmqDto;
using MyEcom.Common.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service.Interfaces
{
    public interface IRabbitMQService
    {
        void SendDataToAdmin(RabbitmqDto reservation);
        void TakeDataToUser(int otelid);
    }
}
