using MVCSlider.Manager;
using MVCSlider.Models;
using PayPal.Api;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.Controllers
{
    public class StripeController : Controller
    {

        private string GetBaseUrl()
        {
            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority;
            return baseURI;
        }

        public ActionResult PaymentWithStripe(string Cancel = null, string subID = null, string questionId = null, string SubChapID = null, string exerciseId = null, string isPastPaper = null, string variant = null)
        {
            StripeTransactionModel md = new StripeTransactionModel();
            //getting the apiContext  

            System.Guid guid = System.Guid.NewGuid();

            var transactionid = guid.ToString();

            var paymentSession = this.CreatePaymentSession(transactionid, questionId, subID, SubChapID, exerciseId, isPastPaper, variant);

            PayPalInvoiceModel model = new PayPalInvoiceModel();
            model.InvoiceGUID = transactionid;
            model.QuestionID = int.Parse(subID);
            model.RequestXML = "Stripe Payment With Session Id = " + paymentSession.Id;
            model.PaymentID = paymentSession.Id;
            
            PayPalManager payPalManager = new PayPalManager();
            payPalManager.CreatePayPalInvoice(model);

            if (variant == null)
            {
                List<Questions> lstRet = new SubjectsController().getChaptersQuesionsBySubjectsID(int.Parse(subID));

                QuestionPayment qp = new QuestionPayment();
                qp.questionid = int.Parse(questionId);
                qp.subjbectid = int.Parse(subID);
                qp.transactionid = transactionid;
                qp.variant = variant;

                ChaptersController questionpayment = new ChaptersController();
                questionpayment.QuestionPaymentSave(qp);
            }
            else
            {
                QuestionPayment qp = new QuestionPayment();
                qp.questionid = int.Parse(questionId);
                qp.subjbectid = int.Parse(subID);
                qp.transactionid = transactionid;
                qp.variant = variant;

                ChaptersController questionpayment = new ChaptersController();
                questionpayment.QuestionPaymentSave(qp);

            }


            md.session = paymentSession;
            md.sessionid = paymentSession.Id;

            var StripePublishablekey = ConfigurationManager.AppSettings["StripePublishablekey"].ToString();

            md.StripePublishablekey = StripePublishablekey;

            return View(md);

        }

        private Session CreatePaymentSession(string transactionid, string questionId, string subID, string SubChapID, string exerciseId, string isPastPaper, string variant)
        {
            var StripeSecretkey = ConfigurationManager.AppSettings["StripeSecretkey"].ToString();

            StripeConfiguration.ApiKey = StripeSecretkey;

            QuestionManager questionManager = new QuestionManager();
            var question = questionManager.GetQuestionByID(int.Parse(questionId));


            var successUrl = "";

            if (isPastPaper != null && isPastPaper == "1")
            {
                TempData["paymentTransactionId"] = transactionid;
                successUrl = string.Format("{0}/Home/ChapterQuestions?sub={1}&questionYearId={2}&variant={3}&scfp=true", GetBaseUrl(), subID, questionId, variant);
            }
            else
            {
                successUrl = string.Format("{0}/Home/ShowVideo?SubChapID={1}&questionId={2}&exerciseId={3}&transactionid={4}", GetBaseUrl(), SubChapID, questionId, exerciseId, transactionid);
            }

            successUrl += "&session_id={ CHECKOUT_SESSION_ID}";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                 "card",
                 },
                LineItems = new List<SessionLineItemOptions> {
                              new SessionLineItemOptions {
                        Name = "Year: " + question.PaperYear,
                        Description = "Student Care Solutions Questions",
                        Amount = 100,
                        Currency = "usd",
                        Quantity = 1,

                    },
                },
                SuccessUrl = successUrl,// "https://example.com/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = string.Format("{0}/Home/Index", GetBaseUrl()),
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    }
}