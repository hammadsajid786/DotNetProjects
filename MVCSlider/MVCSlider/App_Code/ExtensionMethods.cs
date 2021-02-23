using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.App_Code
{
    public static class ExtensionMethods
    {

        public enum UserTypes { Admin, User };

        public static void SendMail()
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("Musamother@gmail.com");
            msg.To.Add("Musamother@gmail.com");
            msg.Subject = "WebSite Access " + DateTime.Now.ToString();
            msg.Body = "Your Site has been Opened";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("Musamother@gmail.com", "hafizabad786");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                var Responsemsg = "Mail has been successfully sent!";
            }
            catch (Exception ex)
            {
                var Responsemsg ="Fail Has error" + ex.Message;
            }
            finally
            {
                msg.Dispose();
            }

            //System.Net.Mail.SmtpClient clsSMTPClient = new System.Net.Mail.SmtpClient();
            //clsSMTPClient.Host = "smtp.gmail.com";
            //clsSMTPClient.Port = 587;
            //clsSMTPClient.EnableSsl = true;
            //System.Net.NetworkCredential clsNetCredentials = new System.Net.NetworkCredential("aghhsolutions@gmail.com", "Pass@word786");
            //clsSMTPClient.Credentials = clsNetCredentials;
            //System.Net.Mail.MailMessage clsMailMessage = new System.Net.Mail.MailMessage();

            //string varEmailTo = "ilghafoor@hotmail.com;huhcommon@gmail.com;";
            //string[] strTo = varEmailTo.Split(';');
            //foreach (string varEmailAddressTo in strTo)
            //{
            //    if (varEmailAddressTo.Trim() != "")
            //    {
            //        clsMailMessage.To.Add(varEmailAddressTo);
            //    }
            //}
            //clsMailMessage.From = new System.Net.Mail.MailAddress("aghhsolutions@gmail.com", "E-Math 77");

            //clsMailMessage.Subject = "E-Math - Main Page Accessed.";
            //clsMailMessage.IsBodyHtml = true;
            //clsMailMessage.Body = @"<p>This is an auto generated notification from E-Math 77 site for home page access</p> </br>";
            //clsSMTPClient.Send(clsMailMessage);

        //    System.Net.Mail.SmtpClient clsSMTPClient = new System.Net.Mail.SmtpClient();

        //    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EmailServerOption"]).ToUpper() == "LIVE")
        //    {

        //        clsSMTPClient.Host = "smtpout.secureserver.net";
        //        clsSMTPClient.Port = 80;
        //        clsSMTPClient.Timeout = 10000;
        //        clsSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //        clsSMTPClient.UseDefaultCredentials = false;
        //        clsSMTPClient.EnableSsl = false;
        //    }
        //    else
        //    {
        //        clsSMTPClient.Host = "smtp.gmail.com";
        //        clsSMTPClient.Port = 587;
        //        clsSMTPClient.EnableSsl = true;
        //    }

        //    System.Net.NetworkCredential clsNetCredentials = new System.Net.NetworkCredential("aghhsolutions@gmail.com", "Pass@word786");
        //    clsSMTPClient.Credentials = clsNetCredentials;
        //    System.Net.Mail.MailMessage clsMailMessage = new System.Net.Mail.MailMessage();

        //    string varEmailTo = "ilghafoor@hotmail.com;huhcommon@gmail.com;";
        //    string[] strTo = varEmailTo.Split(';');
        //    foreach (string varEmailAddressTo in strTo)
        //    {
        //        if (varEmailAddressTo.Trim() != "")
        //        {
        //            clsMailMessage.To.Add(varEmailAddressTo);
        //        }
        //    }
        //    clsMailMessage.From = new System.Net.Mail.MailAddress("aghhsolutions@gmail.com", "E-Math 77");

        //    clsMailMessage.Subject = "E-Math - Main Page Accessed.";
        //    clsMailMessage.IsBodyHtml = true;
        //    clsMailMessage.Body = @"<p>This is an auto generated notification from E-Math 77 site for home page access</p> </br>";
        //    clsSMTPClient.Send(clsMailMessage);

        }

    }
   
}