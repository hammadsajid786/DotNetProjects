using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCSlider.Models
{
    public class Login
    {

        public int userID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LoginTimeStamp { get; set; }
        public DateTime LogOutTimeStamp { get; set; }
        public string IPAddress { get; set; }
        public string LoginStatus { get; set; }
        public string LoginStatusCode { get; set; }


    }
}