using MyEcom.Common.RabbitmqDto;
using MyEcom.Core.Repository;
using MyEcom.Core.Service.Interfaces;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service
{
    public class ReservationService : IReservationService
    {
        public void AddReservation(List<RabbitmqDto> r)
        {
            using (BaseRepository<Reservation> db = new BaseRepository<Reservation>())
            {
                foreach(var item in r)
                {
                    Reservation reservation = new Reservation { checkin = item.checkin,
                        checkout = item.checkout, customerid = item.customerid,
                        hotel_id = item.otelid, roomid = item.roomid,
                        CreateTime = DateTime.Now
                    };

                    db.Add(reservation);
                 
                }
              
            }
        }

        public List<RabbitmqDto> GetReservation(int hotelid)
        {
            throw new NotImplementedException();
        }
        public List<ReservationViewDto> AllReservation()
        {
            List<ReservationViewDto> reservationlist = new List<ReservationViewDto>();
            using (BaseRepository<Reservation> db =new BaseRepository<Reservation>())
            {
                int hotelid = Session.SessionSet<Hotel>.Get("login").Id;
                var reser = db.Query<Reservation>().Where(a => a.hotel_id == hotelid).ToList();

                


                foreach (var item in reser)
                {
                    var room = db.Query<Room>().Where(x => x.Id == item.roomid).FirstOrDefault();
                    var custom = db.Query<Customer>().Where(t => t.Id == item.customerid).FirstOrDefault();
                    ReservationViewDto r = new ReservationViewDto
                    {
                      checkin=item.checkin,
                       checkout=item.checkout,
                        Name=custom.Name,
                         Phone=custom.Phone,
                          Email=custom.Email,
                            roomid=item.roomid,
                        RoomImage = db.Query<Image>().Where(c=>c.Id==room.RoomContent.ImageId).FirstOrDefault().Name,

                    };
                    reservationlist.Add(r);

                }

                
            }
            return reservationlist;
        }

       
    }
}
