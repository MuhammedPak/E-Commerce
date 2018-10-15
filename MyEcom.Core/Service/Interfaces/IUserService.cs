using MyEcom.Common.RedisDto;
using MyEcom.Common.UserDto;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service.Interfaces
{
    public interface IUserService
    {
        List<UserHomePageDto> AllHotels();
        UserHomePageDto SingleHotel(int id);

        void Register(Customer customer);

        void Reservation(ReservationDto reservation);

    }
}
