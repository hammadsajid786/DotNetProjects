using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class QuestionPayment
    {
        public int QuestionPaymentId { get; set; }
        public int subjbectid { get; set; }
        public int questionid { get; set; }
        public string transactionid { get; set; }
        public DateTime create_date { get; set; }
        public string variant { get; set; }
    }
}