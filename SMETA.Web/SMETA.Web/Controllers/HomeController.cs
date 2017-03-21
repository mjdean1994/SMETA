using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SMETA.DataAccess.Repositories;
using SMETA.DataAccess.Models;
using SMETA.Web.ViewModels;

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

        [HttpGet]
        public JsonResult GetData()
        {
            PostRepository postRepository = new PostRepository();
            List<Post> posts = postRepository.GetAllPosts();

            var vm = posts.GroupBy(x => x.PostedDate.Minute).Select(y => new DataViewModel
            {
                date = y.Max(z => z.PostedDate.ToString("dd-MMM-yy HH:mm")),
                positivity = y.Average(z => z.Positivity).ToString(),
                neutrality = y.Average(z => z.Neutrality).ToString(),
                negativity = y.Average(z => z.Negativity).ToString(),
                sentiment = y.Average(z => z.Sentiment).ToString(),
            }).ToArray();

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}