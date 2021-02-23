using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.Controllers
{
    public class EasyPaisaController : Controller
    {
        // GET: EasyPaisa
        public ActionResult Index()
        {
            
            return View();
        }

        public async Task<string> Method1()
        {
            using (var client = new HttpClient())
            {
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("storeId", "70126"));
                values.Add(new KeyValuePair<string, string>("amount", "10"));
                values.Add(new KeyValuePair<string, string>("postBackURL", "http://localhost:50419/easypaisa/response"));
                values.Add(new KeyValuePair<string, string>("orderRefNum", "1101"));
                values.Add(new KeyValuePair<string, string>("expiryDate", DateTime.Now.AddHours(1).ToString("MM/dd/yyyy h:mm:ss")));
                values.Add(new KeyValuePair<string, string>("merchantHashedReq", "FTP0EKH68SWIJC5K"));
                values.Add(new KeyValuePair<string, string>("autoRedirect", "0"));
                values.Add(new KeyValuePair<string, string>("paymentMethod", "MA_PAYMENT_METHOD"));
                values.Add(new KeyValuePair<string, string>("emailAddr", "musamother@gmail.com"));
                values.Add(new KeyValuePair<string, string>("mobileNum", "923024344565"));
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://easypay.easypaisa.com.pk/easypay/Index.jsf", content);
                var responseString =  await response.Content.ReadAsStringAsync();
                return responseString;
            }

        }

        public void   response()
        {
            using (var client = new HttpClient())
            {
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("auth_token",Request.Form["auth_token"]));
                values.Add(new KeyValuePair<string, string>("postBackURL", "http://localhost:50419/easypaisa/response"));
                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("https://easypay.easypaisa.com.pk/easypay/Confirm.jsf", content); 
                //var responseString = response.content.ReadAsStringAsync();
            }
        }
    }
}