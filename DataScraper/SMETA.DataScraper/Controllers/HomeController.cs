using SMETA.DataScraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMETA.DataScraper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Status = TwitterStreamingService.Status;
            return View();
        }

        [HttpPost]
        public JsonResult ToggleStream()
        {
            if(TwitterStreamingService.Status)
            {
                TwitterStreamingService.StopStream();
            }
            else
            {
                TwitterStreamingService.StartStream();
            }

            return Json(new { success = true });
        }
    }
}