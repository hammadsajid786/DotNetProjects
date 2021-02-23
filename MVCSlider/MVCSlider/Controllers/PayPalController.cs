using MVCSlider.Manager;
using MVCSlider.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.Controllers
{
    public class PayPalController : Controller
    {
        private string GetBaseUrl()
        {
            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority;
            return baseURI;
        }

        public ActionResult PaymentWithPaypal(string Cancel = null, string subID = null
            , string questionId = null,
          string SubChapID = null, string exerciseId = null, string isPastPaper = null, string variant = null
            )
        {
            //var smtp = new SmtpClient
            try
            {
                //Create the msg object to be sent
                MailMessage msg = new MailMessage();
                //Add your email address to the recipients
                msg.To.Add("musamother@gmail.com");
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("mychoice_asjad@hotmail.com");
                msg.From = address;
                msg.Subject = "Clicked On Paypal Button on date time " + DateTime.Now;
                msg.Body = "Clicked On Paypal Button on date time " + DateTime.Now;

                SmtpClient client = new SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;
                //Setup credentials to login to our sender email address ("UserName", "Password")
                client.UseDefaultCredentials = false;
                NetworkCredential credentials = new NetworkCredential("mychoice_asjad@hotmail.com", "asjad001$");
                client.Credentials = credentials;

                //Send the msg
                client.Send(msg);

                //Display some feedback to the user to let them know it was sent
                //lblResult.Text = "Your message was sent!";
            }
            catch (Exception ex)
            {
                //If the message failed at some point, let the user know
                //lblResult.Text = ex.ToString(); //alt text "Your message failed to send, please try again."
            }
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            var transactionid = "";
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    //  string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/PaymentWithPayPal?";
                    string baseURI = GetBaseUrl() + "/PayPal/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    System.Guid guid1 = System.Guid.NewGuid();
                    var guid = guid1.ToString();
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, guid, questionId
                        , subID, SubChapID, exerciseId, isPastPaper
                        );
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  

                    string paymentXML = Utility.Serialize<Payment>(createdPayment);

                    PayPalManager payPalManager = new PayPalManager();

                    PayPalInvoiceModel model = new PayPalInvoiceModel();

                    model.InvoiceGUID = guid;
                    model.QuestionID = int.Parse(questionId);
                    model.RequestXML = paymentXML;
                    model.PaymentID = createdPayment.id;

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

                    payPalManager.CreatePayPalInvoice(model);

                    // Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    PayPalManager payPalManager = new PayPalManager();

                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    transactionid = guid;
                    try
                    {
                        var invoice = payPalManager.GetPayPalInvoiceByGUID(guid);

                        if (invoice != null)
                        {
                            var executedPayment = ExecutePayment(apiContext, payerId, invoice.PaymentID as string);

                            var response = Utility.Serialize<Payment>(executedPayment);

                            invoice.State = executedPayment.state;
                            invoice.ResponseXML = response;

                            payPalManager.UpdatePayPalInvoice(invoice);

                            if (executedPayment.state.ToLower() != "approved")
                            {
                                return RedirectToAction("Index", "Home");
                                //   return View("FailureView");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                            // return View("FailureView");

                        }
                    }
                    catch (Exception ex)
                    {
                        var exception = Utility.Serialize<Exception>(ex);
                        payPalManager.LogException(guid, exception);
                        return RedirectToAction("Index", "Home");
                    }
                    //If executed payment failed then we will show payment failure message to user  

                }
            }
            catch (Exception ex)
            {
                //return View("FailureView");
                return RedirectToAction("Index", "Home");
            }
            //on successful payment, show success page to user.  

            if (isPastPaper != null && isPastPaper == "1")
            {
                TempData["paymentTransactionId"] = transactionid;
                return RedirectToAction("ChapterQuestions", "Home", new { sub = subID, questionYearId = questionId, scfp = true });
                // return RedirectToAction("ShowVideoPastPaper", "Home", new { subID = subID, questionId = questionId, transactionid = transactionid });

            }
            else
            {

                return RedirectToAction("ShowVideo", "Home", new { SubChapID = SubChapID, questionId = questionId, exerciseId = exerciseId, transactionid = transactionid });
            }



        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string invoiceNo,
            string questionId = null, string subID = null
            ,
          string SubChapID = null, string exerciseId = null, string isPastPaper = null

            )
        {
            QuestionManager questionManager = new QuestionManager();
            var question = questionManager.GetQuestionByID(int.Parse(questionId));
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Year: " + question.PaperYear,
                currency = "USD",
                price = "1",
                quantity = "1",
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                //cancel_url = string.Format("{0}&Cancel=true&subID={1}&questionId={2}&SubChapID={3}&exerciseId={4}",
                //redirectUrl, subID, questionId, SubChapID, exerciseId),

                cancel_url = string.Format("{0}/Home/Index", GetBaseUrl()),


                return_url = string.Format("{0}&Cancel=false&subID={1}&questionId={2}&SubChapID={3}&exerciseId={4}&isPastPaper={5}",
                redirectUrl, subID, questionId, SubChapID, exerciseId, isPastPaper
                ),

            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "1", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Solution Video For Question",
                invoice_number = "Invoice No " + invoiceNo, //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}