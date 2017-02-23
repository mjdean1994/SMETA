using SMETA.SentimentAnalysisPlatform.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SMETA.SentimentAnalysisPlatform.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        
        [System.Web.Http.HttpGet]
        public JsonResult GetSentiment(string tweetBody)
        {
            return Json(SentimentAnalysisService.Analyze(tweetBody), JsonRequestBehavior.AllowGet);
        }
    }
}
