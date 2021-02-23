using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSlider.App_Code;
using MVCSlider.DAC;
using MVCSlider.Models;

namespace MVCSlider.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["LoginID"])))
            {
                Response.Redirect("~/Home/Index");
            }
            else if (Convert.ToString(Session["UserType"]) != Convert.ToString(ExtensionMethods.UserTypes.Admin))
            {
                Response.Redirect("~/Home/Index");
            }


            return View();
        }
        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(Login modelLogin)
        {
            List<object> lstModels = new List<object>();
            List<KeyValuePair<string, object>> lstData;
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            lstData = new List<KeyValuePair<string, object>>()
            {
            new KeyValuePair<string,object>("UserName",Session["UserName"].ToString()),
            new KeyValuePair<string,object>("UserPassword",Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(modelLogin.Password)))
            };

            dt_1 = clsDAC.Read_DT_Procedure("sp_update_login_password", lstData);

            return View("Index");
        }
    }
}