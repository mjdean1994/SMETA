using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SMETA.DataAccess.Repositories;
using SMETA.DataAccess.Models;
using SMETA.Web.ViewModels;
using SMETA.Web.Services;

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
        
        [HttpPost]
        public JsonResult GetData(DateTime startDate, DateTime endDate, string query, string interval, string username)
        {
            PostRepository postRepository = new PostRepository();
            PostFilter filter = new PostFilter();
            filter.StartDate = startDate;
            filter.EndDate = endDate;
            filter.Query = query;
            filter.Username = username;
            
            List<Post> posts = postRepository.GetPosts(filter);

            IEnumerable<IGrouping<DateTime, Post>> group;

            if(interval == "hour")
            {
                group = GroupingService.GroupByHour(posts);
            }
            else if(interval == "day")
            {
                group = GroupingService.GroupByDay(posts);
            }
            else if(interval == "month")
            {
                group = GroupingService.GroupByMonth(posts);
            }
            else
            {
                group = GroupingService.GroupByYear(posts);
            }

            var vm = group
            .Select(y => new DataViewModel
            {
                date = y.Key.ToString("dd-MMM-yy HH:mm"),
                positivity = y.Average(z => z.Positivity).ToString(),
                neutrality = y.Average(z => z.Neutrality).ToString(),
                negativity = y.Average(z => z.Negativity).ToString(),
                sentiment = y.Average(z => z.Sentiment).ToString(),
            }).ToArray();

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        
    }
}