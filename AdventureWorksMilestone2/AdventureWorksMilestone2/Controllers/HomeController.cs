using AdventureWorksMilestone2.Models;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorksMilestone2.Controllers
{
    public class HomeController : Controller
    {

        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();


        public ActionResult Index()
        {
            ViewBag.images = (from x in db.Products select x.ThumbNailPhoto).Distinct().ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Images = "Your contact page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [CaptchaValidator]
        public async Task<ActionResult> Contact(EmailFormModel model, bool captchaValid)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("johnnytesterino@gmail.com")); 
                message.Subject = "Customer Contact";
                message.Body = string.Format(body, model.FromFirstName + " " + model.FromLastName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            else
            {
                if (!captchaValid)
                {
                    ViewBag.cError = "Captcha is not valid.";
                }
                return View();
            }
        }


        public ActionResult Sent()
        {
            return View();
        }

    }
}