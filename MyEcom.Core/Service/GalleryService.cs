using MyEcom.Common.RedisDto;
using MyEcom.Common.RoomDto;
using MyEcom.Core.Repository;
using MyEcom.Core.Service.Interfaces;
using MyEcom.Core.Session;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service
{
    public class GalleryService : IGalleryService
    {
        public void AddImage(string name)
        {
            using(BaseRepository<Image> db = new BaseRepository<Image>())
            {
                Image image = new Image { Name = name, CreateTime = DateTime.Now };
                db.Add(image);
            }
        }
        public void AddHotelImage(string name)
        {
            
                                   
            using (BaseRepository<Hotel_Image> db = new BaseRepository<Hotel_Image>())
            {
                Hotel_Image hotel_image = new Hotel_Image
                {
                    HotelId = Session.SessionSet<Hotel>.Get("login").Id,
                    ImageId = db.Query<Image>().Where(a => a.Name == name).FirstOrDefault().Id

                };
                db.Add(hotel_image);
            }
            Services.RedisService.SaveHotel();
        }
        public void DeleteRoom(int id)
        {
            using (BaseRepository<Room> db = new BaseRepository<Room>())
            {
                var room = db.Find(id);
                room.IsDeleted = true;
                db.Update(room);
                
            }
         Services.RedisService.SaveHotel();
           
        }
     

        public IEnumerable<HotelImageList> GetHotelImageList()
        {
            using(BaseRepository<Hotel_Image> db=new BaseRepository<Hotel_Image>())
            {
                int hotelid = Session.SessionSet<Hotel>.Get("login").Id;
                return (from hi in db.Query<Hotel_Image>()
                        join i in db.Query<Image>() on hi.Image.Id equals i.Id                    
                        where hi.IsDeleted==false && hi.HotelId==hotelid
                        select new HotelImageList
                        {   Active=hi.Active,
                            ImageName =i.Name,
                            ImageId=i.Id,
                            otelid=hi.HotelId
                        }).ToList();
            }
        
        }


        public void DeleteHotelImage(int id)
        {
          using(BaseRepository<Hotel_Image> db =new BaseRepository<Hotel_Image>())
            {
                var hotel_image = db.Query<Hotel_Image>().Where(a => a.ImageId == id).FirstOrDefault();
                
                hotel_image.IsDeleted = true;
                db.Update(hotel_image);
            }
            Services.RedisService.SaveHotel();
        }
    }
}
