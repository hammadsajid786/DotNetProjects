using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class PayPalInvoiceModel
    {
        public int PayPalInvoiceID { get; set; }
        public string InvoiceGUID { get; set; }
        public int QuestionID { get; set; }
        public string RequestXML { get; set; }
        public string PaymentID { get; set; }
        public string State { get; set; }
        public string ResponseXML { get; set; }
    }
}