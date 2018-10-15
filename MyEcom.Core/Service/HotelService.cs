using MyEcom.Core.Repository;
using MyEcom.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using MyEcom.Core.Session;
using MyEcom.Common.RoomDto;
using System.Web;
using System.IO;
using MyEcom.Common.RedisDto;

namespace MyEcom.Core.Service
{
    public class HotelService : IHotelService
    {
        public void AddHotel(Hotel hotel)
        {

            int hotelid = 0;
            using (BaseRepository<Hotel> db = new BaseRepository<Hotel>())
            {
                hotel.CreateTime = DateTime.Now;
                db.Add(hotel);
                hotelid = db.Query<Hotel>().OrderByDescending(a => a.Id).FirstOrDefault().Id;
            }
            using (BaseRepository<HotelContent> db = new BaseRepository<HotelContent>())
            {
                HotelContent hc = new HotelContent
                {
                    CreateTime = DateTime.Now,
                    Gym = false,
                    HotelId = hotelid,
                    Pool = false,
                    Restaurant = false,
                    RoomService = false,
                    Spa = false,

                };
                db.Add(hc);
            }

        }
        public void SetSession(string email, string password)
        {
            using (BaseRepository<Hotel> db = new BaseRepository<Hotel>())
            {
                var hotel = db.Query<Hotel>().Where(a => a.Email == email && a.Password == password).FirstOrDefault();
                SessionSet<Hotel>.Set(hotel, "login");
            }

        }
        public Hotel GetSession(string email, string password)
        {
            return SessionSet<Hotel>.Get("login");
        }


        public void NewRoom(RoomDto r, string image)
        {
            int hotelid = SessionSet<Hotel>.Get("login").Id;
            var roomcontent = new RoomContent();
            var imagecontent = new Image();
            using (BaseRepository<Image> db = new BaseRepository<Image>())
            {
                Image i = new Image
                {
                    Name = image,
                    CreateTime = DateTime.Now
                };
                db.Add(i);

                imagecontent = db.Query<Image>().Where(a => a.Name == image).FirstOrDefault();

            }



            using (BaseRepository<RoomContent> db = new BaseRepository<RoomContent>())
            {
                RoomContent rc = new RoomContent
                {

                    Aircon = r.Aircon,
                    Bathroom = r.Bathroom,
                    Minibar = r.MiniBar,
                    TV = r.TV,
                    Wifi = r.Wifi,
                    Jacuzzi = r.Jacuzzi,
                    PersonCount = r.Bed,
                    CreateTime = DateTime.Now,
                    ImageId = imagecontent.Id

                };
                db.Add(rc);
                roomcontent = db.Query<RoomContent>().OrderByDescending(a => a.Id).FirstOrDefault();





            }

            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                Room room = new Room
                {
                    RoomType = r.RoomType,
                    Stock = r.RoomNumber,
                    Price = r.Price,
                    HotelId = hotelid,
                    //  RoomContentId=roomcontent,
                    CreateTime = DateTime.Now,
                    RoomContentId = roomcontent.Id
                };
                db.Add(room);
            }


           
                Services.RedisService.SaveHotel();                           
            
            
        }

        public HotelDto GetHotelDto()
        {
            int id = SessionSet<Hotel>.Get("login").Id;
            HotelDto hotelDto = new HotelDto();
            using (BaseRepository<Hotel> db = new BaseRepository<Hotel>())
            {
                hotelDto.Hotel = db.Find(id);
            }
            using (BaseRepository<HotelContent> db = new BaseRepository<HotelContent>())
            {
                hotelDto.HotelContent = db.Query<HotelContent>().Where(a => a.HotelId == id).FirstOrDefault();
            }
            return hotelDto;
        }

        public void UpdateHotel(HotelViewDto hotelDto)
        {
            int hotelid = SessionSet<Hotel>.Get("login").Id;
            HotelDto hotelredis = new HotelDto();

            using (BaseRepository<Hotel> db = new BaseRepository<Hotel>())
            {
                var hotel = db.Find(hotelid);

                hotel.City = hotelDto.City;
                hotel.Country = hotelDto.Country;
                hotel.Description = hotelDto.Description;
                hotel.Email = hotelDto.Email;
                hotel.Name = hotelDto.Name;
                hotel.CreateTime = DateTime.Now;

                hotelredis.Hotel = hotel;
                db.Update(hotel);
            }
            using (BaseRepository<HotelContent> db = new BaseRepository<HotelContent>())
            {

                var id = db.Query<HotelContent>().Where(a => a.HotelId == hotelid).FirstOrDefault().Id;
                HotelContent hotelcontent = new HotelContent
                {
                    CreateTime = DateTime.Now,
                    HotelId = SessionSet<Hotel>.Get("login").Id,
                    Gym = hotelDto.Gym,
                    Pool = hotelDto.Pool,
                    Restaurant = hotelDto.Restaurant,
                    RoomService = hotelDto.RoomService,
                    Spa = hotelDto.Spa,
                    Id = id
                };
                hotelredis.HotelContent = hotelcontent;
                db.Update(hotelcontent);
            }
            Services.RedisService.SaveHotel();
        }

        public IEnumerable<RoomList> RoomList()
        {
            int hotelid = SessionSet<Hotel>.Get("login").Id;
            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                return db.Query<Room>().Where(a => a.HotelId == hotelid && a.IsDeleted != true).Select(x => new RoomList
                {
                    RoomId = x.Id,
                    RoomType = x.RoomType,
                    Stock = x.Stock,
                    Price = x.Price,
                    ImageName = x.RoomContent.Image.Name,
                }).ToList();
            }
        }

        public RoomDto GetRoom(int id)
        {
            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                Room room = db.Find(id);
                RoomContent roomcontent = db.Query<RoomContent>().Where(a => a.Id == room.RoomContentId).FirstOrDefault();
                RoomDto roomdto = new RoomDto
                {
                    Aircon = roomcontent.Aircon,
                    Bathroom = roomcontent.Bathroom,
                    Bed = roomcontent.PersonCount,
                    Jacuzzi = roomcontent.Jacuzzi,
                    MiniBar = roomcontent.Minibar,
                    Price = room.Price,
                    RoomType = room.RoomType,
                    TV = roomcontent.TV,
                    Wifi = roomcontent.Wifi,
                    RoomNumber = room.Stock,
                    RoomId = id
                };




                return roomdto;
            }
        }

        public void UpdateRoom(RoomDto roomDto)
        {
            int hotelid = SessionSet<Hotel>.Get("login").Id;
            RoomContent roomContent = new RoomContent();
            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                Room room = db.Find(roomDto.RoomId);
                room.Price = roomDto.Price;
                room.RoomType = roomDto.RoomType;
                room.Stock = roomDto.RoomNumber;

                db.Update(room);

                roomContent = db.Query<RoomContent>().Where(a => a.Id == room.RoomContentId).FirstOrDefault();
                roomContent.Jacuzzi = roomDto.Jacuzzi;
                roomContent.TV = roomDto.TV;
                roomContent.Wifi = roomDto.Wifi;
                roomContent.Aircon = roomDto.Aircon;
                roomContent.Bathroom = roomDto.Bathroom;
                roomContent.Minibar = roomDto.MiniBar;
            }
            using (BaseRepository<RoomContent> db = new BaseRepository<RoomContent>())
            {
                db.Update(roomContent);
            }

            RedisDto redisDto = new RedisDto();
            using (BaseRepository<HotelContent> db = new BaseRepository<HotelContent>())
            {
                redisDto.Hotel = SessionSet<Hotel>.Get("login");
                redisDto.HotelContent = db.Query<HotelContent>().Where(a => a.HotelId == hotelid && a.IsDeleted == false).FirstOrDefault();

                redisDto.Room = db.Query<Room>().Where(a => a.HotelId == hotelid).ToList();
            }
            Services.RedisService.SaveHotel();
        }
    }
}
