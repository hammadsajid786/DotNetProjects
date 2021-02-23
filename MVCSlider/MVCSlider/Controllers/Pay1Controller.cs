using MVCSlider.Manager;
using MVCSlider.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.Controllers
{
    public class PayPal1Controller : Controller
    {
        public ActionResult PaymentWithPaypal(string Cancel = null, string questionId = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
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
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    System.Guid guid1 = System.Guid.NewGuid();
                    var guid = guid1.ToString();
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, guid, int.Parse(questionId));
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

                    payPalManager.CreatePayPalInvoice(model);

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    PayPalManager payPalManager = new PayPalManager();
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    var response = Utility.Serialize<Payment>(executedPayment);


                    var invoice = payPalManager.GetPayPalInvoiceByGUID(guid);
                    invoice.State = executedPayment.state;
                    invoice.ResponseXML = response;

                    payPalManager.UpdatePayPalInvoice(invoice);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }

                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
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
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string invoiceNo, int questionId)
        {
            QuestionManager questionManager = new QuestionManager();
            var question = questionManager.GetQuestionByID(questionId);
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
                cancel_url = redirectUrl + "&Cancel=true&questionId=" + questionId,
                return_url = redirectUrl + "&Cancel=false&questionId=" + questionId
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

        //private Payment CreatePayment(APIContext apiContext, string redirectUrl, string invoiceNo)
        //{
        //    //create itemlist and add item objects to it  
        //    var itemList = new ItemList()
        //    {
        //        items = new List<Item>()
        //    };
        //    //Adding Item Details like name, currency, price etc  
        //    itemList.items.Add(new Item()
        //    {
        //        name = "Item Name comes here",
        //        currency = "USD",
        //        price = "1",
        //        quantity = "1",
        //        sku = "sku"
        //    });
        //    var payer = new Payer()
        //    {
        //        payment_method = "paypal"
        //    };
        //    // Configure Redirect Urls here with RedirectUrls object  
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl + "&Cancel=true",
        //        return_url = redirectUrl
        //    };
        //    // Adding Tax, shipping and Subtotal details  
        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "1",
        //        subtotal = "1"
        //    };
        //    //Final amount with details  
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
        //        details = details
        //    };
        //    var transactionList = new List<Transaction>();
        //    // Adding description about the transaction  
        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Transaction description",
        //        invoice_number = "Invoice No " + invoiceNo, //Generate an Invoice No  
        //        amount = amount,
        //        item_list = itemList
        //    });
        //    this.payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };
        //    // Create a payment using a APIContext  
        //    return this.payment.Create(apiContext);
        //}


    }
}