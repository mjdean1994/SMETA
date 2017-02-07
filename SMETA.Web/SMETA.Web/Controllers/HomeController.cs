using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

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
    }
}