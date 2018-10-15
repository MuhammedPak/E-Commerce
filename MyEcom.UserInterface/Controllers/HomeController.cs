using MyEcom.Common.RedisDto;
using MyEcom.Common.UserDto;
using MyEcom.Core.Service;
using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEcom.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Services.ElasticService.CreateIndex();


            var model =Services.UserService.AllHotels();
            return View(model);
        }
        public ActionResult Room(int RoomId, int Otel_id)
        {
            SingleRoomDto s = new SingleRoomDto();
            UserHomePageDto hotel = new UserHomePageDto();
            var model = Services.UserService.AllHotels();
            foreach (var item in model)
            {
                if (item.Id == Otel_id)
                {
                    s.Description = item.Description;
                    hotel = item;
                    s.Otel_id = item.Id;
                    s.HotelName = item.Name;
                    s.City = item.City;
                    s.Country = item.Country;
                    s.ImageList = item.Images.ToList();
                }

            }
            foreach(var room in hotel.Room)
            {
                if (room.RoomId == RoomId)
                {
                    s.RoomId = RoomId; 
                    s.Aircon = room.Aircon;
                    s.Bathroom = room.Bathroom;
                    s.Bed = room.Bed;
                    s.Jacuzzi = room.Jacuzzi;
                    s.Price = room.Price;
                    s.RoomImage = room.RoomImage;
                    s.RoomType = room.RoomType;
                    s.MiniBar = room.MiniBar;
               
                }
            }

         
          
            return View(s);
        }
        public ActionResult Hotel(int id)
        {
            UserHomePageDto hotel = new UserHomePageDto();
            var model = Services.UserService.AllHotels();
           foreach(var item in model)
            {
                if (item.Id == id)
                {
                 hotel=item;
                }
            }
            return View(hotel);
        }
        public ActionResult Reservation(int RoomId, int Otel_id)
        {
            SingleRoomDto s = new SingleRoomDto();
            UserHomePageDto hotel = new UserHomePageDto();
            var model = Services.UserService.AllHotels();
            foreach (var item in model)
            {
                if (item.Id == Otel_id)
                {
                    s.Description = item.Description;
                    hotel = item;
                    s.Otel_id = item.Id;
                    s.HotelName = item.Name;
                    s.City = item.City;
                    s.Country = item.Country;
                    s.ImageList = item.Images.ToList();
                }

            }
            foreach (var room in hotel.Room)
            {
                if (room.RoomId == RoomId)
                {
                    s.RoomId = RoomId;
                    s.Aircon = room.Aircon;
                    s.Bathroom = room.Bathroom;
                    s.Bed = room.Bed;
                    s.Jacuzzi = room.Jacuzzi;
                    s.Price = room.Price;
                    s.RoomImage = room.RoomImage;
                    s.RoomType = room.RoomType;
                    s.MiniBar = room.MiniBar;

                }
            }


            return View(s);
        }
        [HttpPost]
        public ActionResult AddUser(Customer customer,int hotelid,int roomid)
        {
            Services.UserService.Register(customer);

            SingleRoomDto s = new SingleRoomDto();
            UserHomePageDto hotel = new UserHomePageDto();
            var model = Services.UserService.AllHotels();
            foreach (var item in model)
            {
                if (item.Id == hotelid)
                {
                    s.Description = item.Description;
                    hotel = item;
                    s.Otel_id = item.Id;
                    s.HotelName = item.Name;
                    s.City = item.City;
                    s.Country = item.Country;
                    s.ImageList = item.Images.ToList();
                }

            }
            foreach (var room in hotel.Room)
            {
                if (room.RoomId == roomid)
                {
                    s.RoomId = roomid;
                    s.Aircon = room.Aircon;
                    s.Bathroom = room.Bathroom;
                    s.Bed = room.Bed;
                    s.Jacuzzi = room.Jacuzzi;
                    s.Price = room.Price;
                    s.RoomImage = room.RoomImage;
                    s.RoomType = room.RoomType;
                    s.MiniBar = room.MiniBar;

                }
            }


            return RedirectToAction("Reservation",s);
        }
        public ActionResult AddReservation (ReservationDto reservation)
        {
            Services.UserService.Reservation(reservation);
            var model = Services.UserService.AllHotels();
            return RedirectToAction("Index","Home",model);
        }
        [HttpGet]
        public ActionResult Search(string key)
        {
            var model = Services.ElasticService.Search(key);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotels()
        {
            var model = Services.UserService.AllHotels();
            return View(model);
        }

    }
}