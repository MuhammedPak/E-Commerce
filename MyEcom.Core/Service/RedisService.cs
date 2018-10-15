using MyEcom.Common.RedisDto;
using MyEcom.Common.RoomDto;
using MyEcom.Core.Repository;
using MyEcom.Core.Service.Interfaces;
using MyEcom.Core.Session;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEcom.Core.Service
{
    public class RedisService:IRedisService
    {
      
        public RedisDto GetHotel(int id)
        {
            using (var redisManager = new PooledRedisClientManager())
            {
                
                using (var redis = redisManager.GetClient())
                {
                    return redis.Get<RedisDto>("hotel" + id);
                }
            }
        }

        public void SaveHotel()
        {
            var list = UserHomePage();   
            foreach(var item in list)
            {
                item.Images = HotelImages(item.Id);
                item.Room = GetRedisHotelRooms(item.Id);
            }
            
            using (var redisManager = new PooledRedisClientManager())
            {
                
                
                using (var redis = redisManager.GetClient())
                {
                    var data = redis.Get<List<UserHomePageDto>>("hotel");
                    if (data != null)
                    {
                        redis.Set<List<UserHomePageDto>>("hotel", list);
                    }
                    else
                    {
                        redis.Add("hotel", list);
                    }

                }
            }
        }

        public void SaveImage()
        {
            var hotelImage = GetRedisHotelImageList();
            using (var redisManager = new PooledRedisClientManager())
            {
              
                using (var redis = redisManager.GetClient())
                {
                    var data = redis.Get<List<HotelImageList>>("image");
                    if (data != null)
                    {
                        redis.Set<IEnumerable<HotelImageList>>("image",hotelImage);
                    }
                    else
                    {
                        redis.Add("image", hotelImage);
                    }

                }
            }
        }

      

       public List<UserHomePageDto> UserHomePage()
        {
            List<UserHomePageDto> list = new List<UserHomePageDto>();
            using (BaseRepository<HotelContent> db = new BaseRepository<HotelContent>())
            {
                list = db.Query<HotelContent>().Where(a => a.IsDeleted == false).Select(x => new UserHomePageDto
                {
                    Id=x.Hotel.Id,
                    City = x.Hotel.City,
                    Country = x.Hotel.Country,
                    Description = x.Hotel.Description,
                    Gym = x.Gym,
                    Pool = x.Pool,
                    Name = x.Hotel.Name,
                    Restaurant = x.Restaurant,
                    RoomService = x.RoomService,
                    Spa = x.Spa,
                

                }).ToList();

                return list;
            }
        }

        public IEnumerable<HotelImageList> HotelImages(int id)

        {
            using (BaseRepository<Hotel_Image> db = new BaseRepository<Hotel_Image>())
            {
                int hotelid = Session.SessionSet<Hotel>.Get("login").Id;
                return (from hi in db.Query<Hotel_Image>()
                        join i in db.Query<Image>() on hi.Image.Id equals i.Id
                        where hi.IsDeleted == false && hi.HotelId==id
                        select new HotelImageList
                        {
                            Active = hi.Active,
                            ImageName = i.Name,
                            ImageId = i.Id,
                            otelid = hi.HotelId
                        }).ToList();
            }

        }

        public IEnumerable<RoomDto> GetRedisHotelRooms(int hotelid)
        {
            IEnumerable<RoomDto> list = new List<RoomDto>();
            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                list = db.Query<Room>().Where(x => x.IsDeleted == false && x.HotelId==hotelid).Select(a => new RoomDto
                {
                    RoomId = a.Id,
                    Aircon = a.RoomContent.Aircon,
                    Bathroom = a.RoomContent.Bathroom,
                    Bed = a.RoomContent.PersonCount,
                    Jacuzzi = a.RoomContent.Jacuzzi,
                    MiniBar = a.RoomContent.Minibar,
                    Price = a.Price,
                    RoomType = a.RoomType,                  
                }).ToList();
            }
            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                foreach (var item in list)
                {
                    var roomcontentid = db.Find(item.RoomId).Id;

                    item.RoomImage = db.Query<RoomContent>().Where(a => a.Id == roomcontentid).Select(a => a.Image.Name).FirstNonDefault();
            }
            }
           
            return list;
        }
        public IEnumerable<HotelImageList> GetRedisHotelImageList()
        {
            using (BaseRepository<Hotel_Image> db = new BaseRepository<Hotel_Image>())
            {
                int hotelid = Session.SessionSet<Hotel>.Get("login").Id;
                return (from hi in db.Query<Hotel_Image>()
                        join i in db.Query<Image>() on hi.Image.Id equals i.Id
                        where hi.IsDeleted == false
                        select new HotelImageList
                        {
                            Active = hi.Active,
                            ImageName = i.Name,
                            ImageId = i.Id,
                            otelid = hi.HotelId
                        }).ToList();
            }

        }

    }
}
