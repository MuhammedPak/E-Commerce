using MyEcom.Core.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult HotelImages()
        {
            var model = Services.GalleryService.GetHotelImageList();
            return View(model);
        }
        public ActionResult AddHotelImage(HttpPostedFileBase image)
        {
            string _imagename = Path.GetFileName(image.FileName);
            string _path = Path.Combine(Server.MapPath("~/Content/images/"), _imagename);
            image.SaveAs(_path);
            Services.GalleryService.AddImage(_imagename);
            Services.GalleryService.AddHotelImage(_imagename);
            return RedirectToAction("HotelImages","Gallery");
        }
        public ActionResult DeleteRoom(int RoomId)
        {
            Services.GalleryService.DeleteRoom(RoomId);
            return Json(new { success = true, JsonRequestBehavior.AllowGet });
        }
        
        public ActionResult DeleteHotelImage(int ImageId)
        {
            Services.GalleryService.DeleteHotelImage(ImageId);
            return RedirectToAction("HotelImages","Gallery");
        }

    }
}