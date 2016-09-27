using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace KursMataUangRupiah.Controllers
{
    public class KursMataUangController : Controller
    {
        
        // GET: KursMataUang
        public ActionResult Kurs()
        {
            return View();
        }

        public string getDataKurs()
        {
            String jsonString = new WebClient().DownloadString("http://www.adisurya.net/kurs-bca/get");
            JObject json = JObject.Parse(jsonString);
            json = (JObject)json["Data"];
            DataTable kurstable = new DataTable();
            kurstable.Columns.Add("Mata_Uang");
            kurstable.Columns.Add("Jual");
            kurstable.Columns.Add("Beli");
            foreach (var obj in json)
            {
                kurstable.Rows.Add(obj.Key, obj.Value["Jual"].ToString(), obj.Value["Beli"].ToString());
            }
            return JsonConvert.SerializeObject(kurstable);
        }
        
        
    }
}