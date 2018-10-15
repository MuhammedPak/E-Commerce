using MyEcom.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service
{
   public class Services
    {
        public static IHotelService HotelService
        {
            get
            {
                //Todo: Injection
                return (IHotelService)new HotelService();
            }

        }
        public static IGalleryService GalleryService
        {
            get
            {
                //Todo: Injection
                return (IGalleryService)new GalleryService();
            }

        }
        public static IRedisService RedisService
        {
            get
            {
                //Todo: Injection
                return (IRedisService)new RedisService();
            }

        }
        public static IUserService UserService
        {
            get
            {
                //Todo: Injection
                return (IUserService)new UserService();
            }

        }
        public static IRabbitMQService RabbitmqServices
        {
            get
            {
                //Todo: Injection
                return (IRabbitMQService)new RabbitmqService();
            }

        }
        public static IReservationService ReservationService
        {
            get
            {
                //Todo: Injection
                return (IReservationService)new ReservationService();
            }

        }
        public static IElasticService ElasticService
        {
            get
            {
                //Todo: Injection
                return (IElasticService)new ElasticService();
            }

        }
    }
}
