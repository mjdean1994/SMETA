using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SMETA.Web.Models;

namespace SMETA.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public JsonResult PostContact(string contactName, string contactEmail, string contactMessage)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("mjdean1994@gmail.com", "MmDea1N11");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(contactEmail, contactName);
            mail.To.Add(new MailAddress("mjdean1994@gmail.com"));

            mail.Subject = "New SMETA Contact Message";
            mail.Body = contactMessage;

            smtpClient.Send(mail);

            return Json(new { success = true });
        }

        public JsonResult GetData()
        {
            List<TweetData> data = new List<TweetData>();

            data.Add(new TweetData
            {
                date = "23-Apr-12",
                sentiment = 0.99f,
                anger = 0.43f,
                anticipation = 0.45f,
                disgust = 0.73f,
                fear = 0.28f,
                joy = 0.49f,
                sadness = 0.73f,
                surprise = 0.41f,
                trust = 0.58f
            });


            data.Add(new TweetData
            {
                date = "20-Apr-12",
                sentiment = 0.77f,
                anger = 0.11f,
                anticipation = .73f,
                disgust = 0.44f,
                fear = 0.86f,
                joy = 0.76f,
                sadness = 0.42f,
                surprise = 0.35f,
                trust = 0.68f
            });


            data.Add(new TweetData
            {
                date = "19-Apr-12",
                sentiment = 0.43f,
                anger = 0.43f,
                anticipation = 0.47f,
                disgust = 0.35f,
                fear = 0.11f,
                joy = 0.28f,
                sadness = 0.73f,
                surprise = 0.41f,
                trust = 0.99f
            });


            data.Add(new TweetData
            {
                date = "18-Apr-12",
                sentiment = 0.75f,
                anger = 0.44f,
                anticipation = 0.76f,
                disgust = 0.12f,
                fear = 0.37f,
                joy = 0.63f,
                sadness = 0.45f,
                surprise = 0.24f,
                trust = 0.18f
            });


            data.Add(new TweetData
            {
                date = "17-Apr-12",
                sentiment = 0.38f,
                anger = 0.47f,
                anticipation = 0.22f,
                disgust = 0.37f,
                fear = 0.19f,
                joy = 0.88f,
                sadness = 0.42f,
                surprise = 0.11f,
                trust = 0.01f
            });


            data.Add(new TweetData
            {
                date = "16-Apr-12",
                sentiment = 0.41f,
                anger = 0.06f,
                anticipation = 0.58f,
                disgust = 0.47f,
                fear = 0.11f,
                joy = 0.09f,
                sadness = 0.98f,
                surprise = 0.45f,
                trust = 0.36f
            });


            data.Add(new TweetData
            {
                date = "13-Apr-12",
                sentiment = 0.68f,
                anger = 0.44f,
                anticipation = 0.98f,
                disgust = 0.04f,
                fear = 0.93f,
                joy = 0.86f,
                sadness = 0.41f,
                surprise = 0.88f,
                trust = 0.08f
            });

            data.Add(new TweetData
            {
                date = "12-Apr-12",
                sentiment = 0.36f,
                anger = 0.46f,
                anticipation = 0.36f,
                disgust = 0.05f,
                fear = 0.77f,
                joy = 0.98f,
                sadness = 0.36f,
                surprise = 0.22f,
                trust = 0.77f
            });

            data.Add(new TweetData
            {
                date = "11-Apr-12",
                sentiment = 0.02f,
                anger = 0.80f,
                anticipation = 0.36f,
                disgust = 0.59f,
                fear = 0.18f,
                joy = 0.63f,
                sadness = 0.82f,
                surprise = 0.76f,
                trust = 0.66f
            });

            data.Add(new TweetData
            {
                date = "10-Apr-12",
                sentiment = 0.32f,
                anger = 0.56f,
                anticipation = 0.28f,
                disgust = 0.45f,
                fear = 0.58f,
                joy = 0.85f,
                sadness = 0.22f,
                surprise = 0.52f,
                trust = 0.11f
            });

            data.Add(new TweetData
            {
                date = "9-Apr-12",
                sentiment = 0.47f,
                anger = 0.56f,
                anticipation = .64f,
                disgust = 0.27f,
                fear = 0.77f,
                joy = 0.85f,
                sadness = 0.75f,
                surprise = 0.24f,
                trust = 0.43f
            });

            data.Add(new TweetData
            {
                date = "5-Apr-12",
                sentiment = 0.91f,
                anger = 0.56f,
                anticipation = .05f,
                disgust = 0.44f,
                fear = 0.82f,
                joy = 0.76f,
                sadness = 0.43f,
                surprise = 0.43f,
                trust = 0.72f
            });

            data.Add(new TweetData
            {
                date = "4-Apr-12",
                sentiment = 0.57f,
                anger = 0.56f,
                anticipation = 0.47f,
                disgust = 0.73f,
                fear = 0.18f,
                joy = 0.84f,
                sadness = 0.86f,
                surprise = 0.74f,
                trust = 0.52f
            });


            data.Add(new TweetData
            {
                date = "3-Apr-12",
                sentiment = 0.69f,
                anger = 0.77f,
                anticipation = .68f,
                disgust = 0.32f,
                fear = 0.78f,
                joy = 0.14f,
                sadness = 0.8f,
                surprise = 0.58f,
                trust = 0.47f
            });


            data.Add(new TweetData
            {
                date = "2-Apr-12",
                sentiment = 0.19f,
                anger = 0.56f,
                anticipation = 0.78f,
                disgust = 0.49f,
                fear = 0.58f,
                joy = 0.01f,
                sadness = 0.22f,
                surprise = 0.52f,
                trust = 0.11f
            });


            data.Add(new TweetData
            {
                date = "30-Mar-12",
                sentiment = 0.5f,
                anger = 0.99f,
                anticipation = 0.98f,
                disgust = 0.11f,
                fear = 0.58f,
                joy = 0.85f,
                sadness = 0.10f,
                surprise = 0.72f,
                trust = 0.01f
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}