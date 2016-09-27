using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Mail;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using FluentScheduler;

namespace KursMataUangRupiah
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //fluent scheduller
            var registry = new Registry();
            registry.Schedule(sendMail).ToRunNow().AndEvery(1).Seconds();
            JobManager.Initialize(registry);

        }
        public void sendMail()
        {
            String jsonString = new WebClient().DownloadString("http://www.adisurya.net/kurs-bca/get");
            JObject json = JObject.Parse(jsonString);
            json = (JObject)json["Data"];
            string textgabungan = "<html><p>Kurs Mata Uang Rupiah</p><table border='1'><tr><td></td><td colspan='2'>Harga</td></tr><tr><td>Mata Uang</td><td>Jual</td><td>Beli</td></tr>";
            foreach (var obj in json)
            {
                textgabungan += "<tr><td>" + obj.Key + "</td><td>" + obj.Value["Jual"].ToString() + "</td><td>" + obj.Value["Beli"].ToString() + "</td></tr>";
            }
            textgabungan += "</table></html>";
            var smtpclient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("lutfiatest@gmail.com", "testtesttesttest"),
                EnableSsl = true

            };
            MailMessage mm = new MailMessage("lutfiarahmanda@gmail.com", "lutfiatest@gmail.com");
            mm.IsBodyHtml = true;
            mm.Subject = "TEST MAIL";
            mm.Body = textgabungan;

            smtpclient.Send(mm);
        }
    }
}
