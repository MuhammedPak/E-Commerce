using MyEcom.Common.RabbitmqDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service.Interfaces
{
   public interface IReservationService
    {
        void AddReservation(List<RabbitmqDto> r);
        List<RabbitmqDto> GetReservation(int hotelid);
        List<ReservationViewDto> AllReservation();
    }
}
