using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class StripeTransactionModel
    {
        public Stripe.Checkout.Session session { get; set; }
        public string sessionid { get; set; }
       
        public string StripeSecretkey { get; internal set; }
        public string StripePublishablekey { get; internal set; }
    }
}