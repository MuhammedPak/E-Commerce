using MyEcom.Common.RabbitmqDto;
using MyEcom.Common.RedisDto;
using MyEcom.Common.UserDto;
using MyEcom.Core.DatabaseContext;
using MyEcom.Core.Repository;
using MyEcom.Core.Service.Interfaces;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service
{
    public class UserService:IUserService
    {
        public List<UserHomePageDto> AllHotels()
        {
            UserDto userdto = new UserDto();

            using (var redisManager = new PooledRedisClientManager())
            {
                var redis = redisManager.GetClient();

                return (redis.Get<List<UserHomePageDto>>("hotel"));
            }
        }

        public void Reservation(ReservationDto r)
        {
            
            int customer = 0;
            using(BaseRepository<Customer>db=new BaseRepository<Customer>())
            {
            customer = db.Query<Customer>().Where(a => a.Name == r.name && a.Password == r.password).FirstOrDefault().Id;
            }
            if (customer != 0)
            {
                     RabbitmqDto dto = new RabbitmqDto {checkin=r.checkin,checkout=r.checkout, customerid=customer,otelid=r.otelid,roomid=r.roomid };
                      Services.RabbitmqServices.SendDataToAdmin(dto);
            }
           
        }

        public void Register(Customer customer)
        {
          using (BaseRepository<Customer> db = new BaseRepository<Customer>())
            {
                customer.CreateTime = DateTime.Now;
                db.Add(customer);
            } 

        }

        public UserHomePageDto SingleHotel(int id)
        {
            throw new NotImplementedException();
        }

       
    }

}
