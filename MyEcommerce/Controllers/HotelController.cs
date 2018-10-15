using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcom.Core.Service;
using ServiceStack.Redis;
using Newtonsoft.Json;
using MyEcom.Common.RoomDto;
using MyEcom.Core.Session;
using System.IO;




namespace MyEcommerce.Controllers
{
    public class HotelController : Controller
    {
        // GET: Hotel
        public ActionResult Index()
        {   
            return View();
        }
        public ActionResult RegisterHotel(Hotel hotel)
        {
            Services.HotelService.AddHotel(hotel);
            return RedirectToAction("Index","Home");
        }
        public ActionResult Login(string email, string password)
        {
             Services.HotelService.SetSession(email, password);


             var model=Services.RedisService.GetHotel(SessionSet<Hotel>.Get("login").Id);

            return RedirectToAction("HotelDetail","Hotel");
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult NewRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewRoom(RoomDto roomDto,HttpPostedFileBase image)
        {

            string _imagename = Path.GetFileName(image.FileName);
            string _path = Path.Combine(Server.MapPath("~/Content/images/"), _imagename);
            image.SaveAs(_path);

            Services.HotelService.NewRoom(roomDto, _imagename);

            return RedirectToAction("RoomList","Hotel");
        }       
        public ActionResult HotelDetail()
        {
            var model=Services.HotelService.GetHotelDto();
            return View(model);
        }
        public ActionResult UpdateHotel(HotelViewDto hotel)
        {

            Services.HotelService.UpdateHotel(hotel);
            var model = Services.HotelService.GetHotelDto();
            return RedirectToAction("HotelDetail", "Hotel");
        }
        public ActionResult RoomList()
        {  
            
            var model = Services.HotelService.RoomList();
            return View(model);
        }
        public ActionResult GetRoom(int RoomId)
        {
          var model=Services.HotelService.GetRoom(RoomId);
            return View(model);
        }
        public ActionResult RoomUpdate(RoomDto roomDto)
        {
            Services.HotelService.UpdateRoom(roomDto);
            return RedirectToAction("RoomList", "Hotel");
        }
        public ActionResult Reservations()
        {

            
            return View();

        }
        public PartialViewResult ReservationList()
        {
            int id = SessionSet<Hotel>.Get("login").Id;
            Services.RabbitmqServices.TakeDataToUser(id);
            var model = Services.ReservationService.AllReservation();

            return PartialView("_Reservation",model);
        }
     
    }

}      
























/*  using (var redisManager = new PooledRedisClientManager())
            using (var redis = redisManager.GetClient())
            {
                Hotel h = new Hotel
                {
                    Email = email,
                    Password = password
                };
               var redisTodos =JsonConvert.SerializeObject( redis.As<Hotel>());
                redis.Add(Guid.NewGuid().ToString(), redisTodos);
                //for (int i = 0; i < 10; i++)
                //{
                //    var todo = new Todo
                //    {
                //        Name = "Osman" + i
                //    };
                //    redis.Add(Guid.NewGuid().ToString(),todo);
                //}
                var keys = redis.GetAllKeys();
                var data = redis.GetAll<Hotel>(keys);
                var a = data;


                // var rnd = new Random();
                //var t= rnd.Next(100);
                //

                // redisTodos.Store(todo);
                // redisTodos.Save();
                //var data= redisTodos.GetAll();
                // Console.WriteLine(data);
                //var data=redis.Get<Todo>("test");
            }


            Console.ReadLine();      

                          */
