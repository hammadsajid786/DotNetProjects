using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;
using MVCSlider.App_Code;
using MVCSlider.Manager;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Net;

namespace MVCSlider.Controllers
{
    public class HomeController : Controller
    {
        private string GetBaseUrl()
        {
            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority;
            return baseURI;
        }

        public ActionResult Index()
        {
            

            List<object> lstModels = new List<object>();
            lstModels.Add(new Login());
            List<Subjects> modelSubjectsList = new SubjectsController().getSubjects();
            List<Subjects> modelSubjectsTopicsList = new SubjectsController().getSubjectTopics();
            lstModels.Add(modelSubjectsList);
            lstModels.Add(modelSubjectsTopicsList);

            if (Session["EmailSent"] == null)
            {
                try
                {
                    MVCSlider.App_Code.ExtensionMethods.SendMail();
                    Session["EmailSent"] = "Sent";
                }
                catch
                {

                }
            }

            if (Session["TotalVisitors"] == null)
            {
                try
                {
                    int Counter = MVCSlider.DAC.MSSQL_DAC.SaveVisitor();
                    Session["TotalVisitors"] = Counter;
                    ViewBag.TotalVisitors = Counter;
                }
                catch
                {
                    ViewBag.TotalVisitors = 0;
                }
            }
            else
            {
                ViewBag.TotalVisitors = Session["TotalVisitors"] == null ? 0 : Convert.ToInt32(Session["TotalVisitors"]);
            }


            return View("Index", "~/Views/Shared/_IndexPageLayout.cshtml", lstModels);
        }

        [HttpPost]
        public ActionResult Index(Login modelLogin)
        {
            List<object> lstModels = new List<object>();
            List<KeyValuePair<string, object>> lstData;
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            ViewBag.TotalVisitors = Session["TotalVisitors"] == null ? 0 : Convert.ToInt32(Session["TotalVisitors"]);

            //---Getting Subjects
            List<Subjects> modelSubjectsList = new SubjectsController().getSubjects();

            Session["UserName"] = modelLogin.UserName;
            //---Login 
            lstData = new List<KeyValuePair<string, object>>()
            {
            new KeyValuePair<string,object>("UserName",modelLogin.UserName),
            new KeyValuePair<string,object>("UserPassword",Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(modelLogin.Password))),
            new KeyValuePair<string,object>("IPAddress",System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Where(item => item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString())
            };

            dt_1 = clsDAC.Read_DT_Procedure("sp_Login", lstData);

            if (dt_1 == null || dt_1.Rows.Count == 0)
            {
                modelLogin.LoginStatus = "Login Failed: Unable to access";
                modelLogin.LoginStatusCode = "0";
            }
            else if (dt_1 != null && dt_1.Rows.Count > 0)
            {
                modelLogin.userID = Convert.ToInt32(dt_1.Rows[0]["UserID"]);
                modelLogin.LoginStatusCode = Convert.ToString(dt_1.Rows[0]["LoginStatusCode"]);
                modelLogin.LoginStatus = Convert.ToString(dt_1.Rows[0]["LoginStatus"]);
                modelLogin.IPAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Where(item => item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
                modelLogin.LoginTimeStamp = Convert.ToDateTime(dt_1.Rows[0]["LoginTimeStamp"]);
            }

            if (modelLogin.LoginStatusCode != "1")
            {
                lstModels.Add(modelLogin);
                lstModels.Add(modelSubjectsList);
                return View("Index", "~/Views/Shared/_IndexPageLayout.cshtml", lstModels);
            }

            Session["LoginID"] = Convert.ToInt32(dt_1.Rows[0]["LoginID"]);
            Session["UserID"] = modelLogin.userID;
            Session["UserName"] = modelLogin.UserName;
            Session["UserType"] = Convert.ToString(dt_1.Rows[0]["UserType"]);
            Session["UserFirstName"] = Convert.ToString(dt_1.Rows[0]["FirstName"]);
            Session["UserLastName"] = Convert.ToString(dt_1.Rows[0]["LastName"]);
            Session["LoginTimeStamp"] = Convert.ToDateTime(modelLogin.LoginTimeStamp);
            Session["StudentID"] = Convert.ToString(dt_1.Rows[0]["StudentID"]) == "" ? 0 : Convert.ToInt32(dt_1.Rows[0]["StudentID"]);

            if (Convert.ToString(dt_1.Rows[0]["UserType"]) == "Admin")
            {
                Response.Redirect("~/Admin/Index");

            }
            else if (Convert.ToString(dt_1.Rows[0]["UserType"]) == "User")
            {
                Response.Redirect("~/User/Index");
            }

            lstModels.Add(modelLogin);
            lstModels.Add(modelSubjectsList);
            return View("Index", "~/Views/Shared/_IndexPageLayout.cshtml", lstModels);

        }

      
        public int Logout()
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
            {
            new KeyValuePair<string,object>("LoginID", Convert.ToString(Session["LoginID"]))
            };

            dt_1 = clsDAC.Read_DT_Procedure("sp_Logout", lstData);


            if (Session.SessionID != null)
            {
                Session.Clear();
                Session.Abandon();
            }

            Response.Redirect("/Home/Index");
            return 1;
        }

        public ActionResult AboutUs()
        {
            List<object> lstVisitors = new List<object>();
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();
            dt_1 = clsDAC.Read_DT_Query("SELECT Id,VisitingTime FROM tblVisitor");

            if (dt_1 != null || dt_1.Rows.Count != 0)
            {
                foreach (DataRow item in dt_1.Rows)
                {
                    lstVisitors.Add(new { ID = Convert.ToInt32(item["Id"]), VisitingTime = Convert.ToDateTime(item["VisitingTime"]) });
                }
            }

            return View(lstVisitors);
        }

        public ActionResult OurConsultants()
        {
            return View();
        }

        public ActionResult ServicesAndBenefits()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Chapters(int Sub, int IsTopical)
        {
            ChaptersViewModel vm = new ChaptersViewModel();

            List<SubjectChapters> lstRet = new SubjectsController().getChaptersExerciseBySubjectsID(Sub);
            List<Questions> lstRet1 = new SubjectsController().getChaptersQuesionsBySubjectsID(Sub);

            vm.SubjectChapters = lstRet;
            vm.Questions = lstRet1;

            ViewBag.IsTopical = IsTopical;

            return View(vm);
        }

        public ActionResult ChapterQuestions(int Sub, int questionYearId, string variant,bool scfp= false )
        {
            ChaptersViewModel vm = new ChaptersViewModel();
            PayPalManager payPalManager = new PayPalManager();
            int subjecttype;
            if (scfp == false)
            {
                if (TempData["paymentTransactionId"] != null)
                {
                    SubjectsController sc = new SubjectsController();
                    sc.DeleteInvoiceFromPaymentQuestion(Convert.ToString(TempData["paymentTransactionId"]));
                    payPalManager.DeletePayPalInvoice(Convert.ToString(TempData["paymentTransactionId"]));

                    ViewBag.paymentTransactionId = null;
                    TempData["paymentTransactionId"] = null;
                }
                else if (TempData["paymentTransactionId"] == null)
                {                    
                        ViewBag.paymentTransactionId = null;
                        TempData["paymentTransactionId"] = null;                 
                }
            }
           
            if (TempData["paymentTransactionId"] != null)
            {

                var invoice = payPalManager.GetPayPalInvoiceByGUID(Convert.ToString(TempData["paymentTransactionId"]));
                if (invoice != null)
                {
                    if (invoice.QuestionID != Sub)
                    {
                        ViewBag.paymentTransactionId = null;
                    }
                    else if (invoice.QuestionID == Sub && !string.IsNullOrEmpty(variant))
                    {
                        List<QuestionPayment> lstQuestion = new SubjectsController().getQuesionsFromPaymentQuestion_BySubjectsIDAndTransactionAndVarient(Sub, Convert.ToString(TempData["paymentTransactionId"]), variant);
                        if (lstQuestion.Count > 0)
                        {
                            if (lstQuestion.Select(x => x.create_date == DateTime.Now.Date).FirstOrDefault())
                            {
                                ViewBag.paymentTransactionId = TempData["paymentTransactionId"];
                                TempData.Keep("paymentTransactionId");

                                //try
                                //{
                                //    MailMessage msgs = new MailMessage();
                                //    msgs.To.Add("ilghafoor@hotmail.com");
                                //    MailAddress address = new MailAddress("mychoice_asjad@hotmail.com");
                                //    msgs.From = address;
                                //    msgs.Subject = "USer Redirect to Payment";
                                //    string htmlBody = "Sending This email When user click on Payment Button ";
                                //    msgs.Body = htmlBody;
                                //    msgs.IsBodyHtml = true;
                                //    SmtpClient client = new SmtpClient();
                                //    client.Host = "relay-hosting.secureserver.net";
                                //    client.Port = 25;
                                //    client.UseDefaultCredentials = false;
                                //    client.Credentials = new System.Net.NetworkCredential("mychoice_asjad@hotmail.com", "asjad001$");
                                //    //Send the msgs  
                                //    client.Send(msgs);

                                //}
                                //catch (Exception ex) { }
                            }
                            else
                            {
                                ViewBag.paymentTransactionId = null;
                            }
                        }
                        else
                        {
                            ViewBag.paymentTransactionId = null;
                        }
                    }
                    else
                    {
                        List<QuestionPayment> lstQuestion = new SubjectsController().getQuesionsFromPaymentQuestion_BySubjectsIDAndTransaction(Sub, Convert.ToString(TempData["paymentTransactionId"]));
                        if (lstQuestion.Count > 0)
                        {
                            if (lstQuestion.Select(x => x.create_date == DateTime.Now.Date).FirstOrDefault())
                            {
                                ViewBag.paymentTransactionId = TempData["paymentTransactionId"];
                                TempData.Keep("paymentTransactionId");
                                //try
                                //{
                                //    MailMessage msgs = new MailMessage();
                                //    msgs.To.Add("ilghafoor@hotmail.com");
                                //    MailAddress address = new MailAddress("mychoice_asjad@hotmail.com");
                                //    msgs.From = address;
                                //    msgs.Subject = "USer Redirect to Payment";
                                //    string htmlBody = "Sending This email When user click on Payment Button ";
                                //    msgs.Body = htmlBody;
                                //    msgs.IsBodyHtml = true;
                                //    SmtpClient client = new SmtpClient();
                                //    client.Host = "relay-hosting.secureserver.net";
                                //    client.Port = 25;
                                //    client.UseDefaultCredentials = false;
                                //    client.Credentials = new System.Net.NetworkCredential("mychoice_asjad@hotmail.com", "asjad001$");
                                //    //Send the msgs  
                                //    client.Send(msgs);

                                //}
                                //catch (Exception ex) { }
                            }
                            else
                            {
                                ViewBag.paymentTransactionId = null;
                            }
                        }
                        else
                        {
                            ViewBag.paymentTransactionId = null;
                        }
                    }
                }
                else
                {
                    ViewBag.paymentTransactionId = null;
                }
            }
            else
            {
                ViewBag.paymentTransactionId = null;
            }

          

                List<SubjectChapters> lstRet = new SubjectsController().getChaptersExerciseBySubjectsID(Sub);
            List<Questions> lstRet1 = new SubjectsController().getChaptersQuesionsBySubjectsID(Sub);
            var variants1 = lstRet1.Where(x => !string.IsNullOrWhiteSpace(x.ImageFileName)).Select(x => x.ImageFileName).Distinct().ToList();
            subjecttype = lstRet.Select(x => x.SubjectType).FirstOrDefault();
            ViewBag.subjecttype = subjecttype;
            ViewBag.selectedVariant = null;
            
            // var type = lstsubjects.Select(x=>x.SubjectID) lstRet1.Select(x => x.SubjectsChaptersID).FirstOrDefault();
            vm.Variants = GetSelectListItems(variants1);
            var questionYear = lstRet1.Where(x => x.QuestionID == questionYearId).FirstOrDefault()?.PaperYear;
            //var questionYear = lstRet1.FirstOrDefault()?.PaperYear;
            ViewBag.questionYearId = questionYearId;


            //var variatnEndwith2;
            //foreach
            if (subjecttype == 1 || subjecttype == 4 || subjecttype == 5 || subjecttype == 8)
            {
                if (!string.IsNullOrEmpty(variant))
                {
                    lstRet1 = lstRet1.Where(x => x.ImageFileName == variant).ToList();
                    ViewBag.selectedVariant = variant;
                }
                else
                {
                    var variants2 = variants1.Where(x => x.EndsWith("2")).FirstOrDefault();
                    lstRet1 = lstRet1.Where(x => x.ImageFileName == variants2).ToList();
                    ViewBag.selectedVariant = variants2;
                }
            }

           


            vm.SubjectChapters = lstRet;
            vm.Questions = lstRet1.Where(x => x.PaperYear == questionYear).ToList();

            return View(vm);
        }

        private List<SelectListItem> GetSelectListItems(List<string> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
            return selectList;
        }

        public ActionResult PastPapers(int subID = 0)
        {

            List<object> lstRet = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);

            ViewBag.PastPapers = lstRet;

            return View(lstRet);
        }
        public ActionResult ChapterContent(int subID = 0)
        {

            List<object> lstRet = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);
            return View(lstRet);
        }

        public ActionResult SubjectTopic(int subID = 0)
        {

            List<object> lstRet = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);

            ViewBag.PastPapers = lstRet;

            return View(lstRet);
        }

        public ActionResult SatTopic(int subID = 0)
        {

            List<object> lstRet = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);

            ViewBag.PastPapers = lstRet;

            return View(lstRet);
        }

        public ActionResult CSSTopic(int subID = 0)
        {

            List<object> lstRet = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);

            ViewBag.PastPapers = lstRet;

            return View(lstRet);
        }

        public ActionResult ChaptersDetails(int SubChapID)
        {
            List<object> lstRet = new List<object>();
            lstRet = new SubjectsController().getChapterContentsByID(SubChapID);
            return View(lstRet);
        }

        //  public ActionResult ShowVideo(int SubChapID, string name)
        public ActionResult ShowVideo(int SubChapID, int questionId, int exerciseId, string transactionid
            , string session_id = null, bool isdwn = false
            )
        {
            if (TempData["paymentTransactionId"] != null)
            {
                PayPalManager payPalManager = new PayPalManager();
                var invoice = payPalManager.GetPayPalInvoiceByGUID(Convert.ToString(TempData["paymentTransactionId"]));
                if (invoice != null)
                {
                    if (invoice.QuestionID != SubChapID)
                    {

                        ViewBag.paymentTransactionId = null;
                    }
                    else
                    {
                        ViewBag.paymentTransactionId = TempData["paymentTransactionId"];
                    }
                }
                //TempData.Keep("paymentTransactionId"); 
            }
            if (string.IsNullOrEmpty(transactionid))
            {
                if (isdwn == true)
                {
                    List<Questions> Question = new SubjectsController().getChaptersQuesionsByQuestionID(questionId);
                    if (Question.Select(x => x.isfree).FirstOrDefault() == true)
                    {
                        var showVideoViewModel1 = new SubjectsController().getShowVideoViewModelByID(SubChapID).FirstOrDefault();
                        var Exercises1 = new SubjectsController().getShowVideoViewModelByID(exerciseId);
                        var question1 = Exercises1.Where(x => x.ContentsHolder == questionId).FirstOrDefault();
                        ViewBag.Path = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(question1.VideoFilePath);
                        showVideoViewModel1.ExerciseName = Exercises1.FirstOrDefault().ChapterName;
                        showVideoViewModel1.QuestionText = question1.QuestionText;

                        string downloadURL1 = GetBaseUrl() + "/Home/DownloadQuestion?p1=" + questionId;
                        ViewBag.DownloadURL = downloadURL1;
                        ViewBag.transactionid = transactionid;


                        return View("ShowVideo", "~/Views/Shared/_IndexPageLayout.cshtml", showVideoViewModel1);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    return
                  RedirectToAction("Index", "Home");
            }
            else
            {
                PayPalManager payPalManager = new PayPalManager();
                var invoice = payPalManager.GetPayPalInvoiceByGUID(transactionid);
                if (invoice != null && invoice.RequestXML.ToLower().Contains("stripe payment"))
                {
                    if (invoice.QuestionID != questionId && session_id != invoice.PaymentID)
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (invoice == null || invoice.QuestionID != questionId || invoice.State != "approved")
                        return RedirectToAction("Index", "Home");
                }
            }

            var showVideoViewModel = new SubjectsController().getShowVideoViewModelByID(SubChapID).FirstOrDefault();
            var Exercises = new SubjectsController().getShowVideoViewModelByID(exerciseId);
            var question = Exercises.Where(x => x.ContentsHolder == questionId).FirstOrDefault();
            ViewBag.Path = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(question.VideoFilePath);
            showVideoViewModel.ExerciseName = Exercises.FirstOrDefault().ChapterName;
            showVideoViewModel.QuestionText = question.QuestionText;

            string downloadURL = GetBaseUrl() + "/Home/DownloadQuestion?p1=" + questionId;
            ViewBag.DownloadURL = downloadURL;
            ViewBag.transactionid = transactionid;

            try
            {
                string strURL = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(question.VideoFilePath);
                //string strFileName = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFileName);
                WebClient req = new WebClient();
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
                //response.AppendHeader("Content-Disposition", "attachment; filename=" + subjectName +" "+ chapterName + " " + questionText + "");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
            }
            catch (Exception ex)
            {
            }

            return View("ShowVideo", "~/Views/Shared/_IndexPageLayout.cshtml", showVideoViewModel);

        }

        public ActionResult ShowVideoPastPaper(int subID, int questionId, string transactionid, string session_id = null, bool isdwn = false, string variant= null)
        {

            if (TempData["paymentTransactionId"] != null)
            {
                PayPalManager payPalManager = new PayPalManager();
                var invoice = payPalManager.GetPayPalInvoiceByGUID(Convert.ToString(TempData["paymentTransactionId"]));
                if (invoice != null)
                {
                    if (invoice.QuestionID != subID)
                    {
                        ViewBag.paymentTransactionId = null;
                    }
                    else if (invoice.QuestionID == subID || variant != null)
                    {
                        List<QuestionPayment> lstQuestion = new SubjectsController().getQuesionsFromPaymentQuestion_BySubjectsIDAndTransactionAndVarient(subID, Convert.ToString(TempData["paymentTransactionId"]), variant);
                        if (lstQuestion.Count > 0)
                        {
                            if (lstQuestion.Select(x => x.create_date == DateTime.Now.Date).FirstOrDefault())
                            {
                                ViewBag.paymentTransactionId = TempData["paymentTransactionId"];
                                TempData.Keep("paymentTransactionId");

                            }
                            else
                            {
                                ViewBag.paymentTransactionId = null;
                            }
                        }
                        else
                        {
                            ViewBag.paymentTransactionId = null;
                        }
                    }
                    else
                    {
                        List<QuestionPayment> lstQuestion = new SubjectsController().getQuesionsFromPaymentQuestion_BySubjectsIDAndTransaction(subID, Convert.ToString(TempData["paymentTransactionId"]));
                        if (lstQuestion.Count > 0)
                        {
                            if (lstQuestion.Select(x => x.create_date == DateTime.Now.Date).FirstOrDefault())
                            {
                                ViewBag.paymentTransactionId = TempData["paymentTransactionId"];
                                TempData.Keep("paymentTransactionId");

                            }
                            else
                            {
                                ViewBag.paymentTransactionId = null;
                            }
                        }
                        else
                        {
                            ViewBag.paymentTransactionId = null;
                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(transactionid))
            {
                if (isdwn == true)
                {
                    List<Questions> Question = new SubjectsController().getChaptersQuesionsByQuestionID(questionId);
                    if (Question.Select(x => x.isfree).FirstOrDefault() == true)
                    {
                        List<object> lstRet1 = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);
                        var showVideoViewModel1 = new ShowVideoViewModel();
                        var videoFileName1 = "";
                        var videoFilePath1 = "";
                        var subjectName1 = "";
                        var chapterName1 = "";
                        var questionText1 = "";

                        foreach (var item in lstRet1)
                        {
                            var questionIdDB = item.GetType().GetProperty("ContentsHolder").GetValue(item, null).ToString();

                            if (questionId.ToString() == questionIdDB)
                            {
                                videoFilePath1 = item.GetType().GetProperty("VideoFilePath").GetValue(item, null).ToString();
                                videoFileName1 = item.GetType().GetProperty("VideoFileName").GetValue(item, null).ToString();
                                subjectName1 = item.GetType().GetProperty("SubjectName").GetValue(item, null).ToString();
                                chapterName1 = item.GetType().GetProperty("ChapterName").GetValue(item, null).ToString();
                                questionText1 = item.GetType().GetProperty("QuestionText").GetValue(item, null).ToString();
                            }
                        }

                        ViewBag.Path = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFilePath1);


                        showVideoViewModel1.SubjectName = subjectName1;
                        showVideoViewModel1.ChapterName = chapterName1;

                        showVideoViewModel1.ExerciseName = chapterName1;
                        showVideoViewModel1.QuestionText = questionText1;

                        ViewBag.ShowVideoPastPaper = 1;

                        string downloadURL1 = GetBaseUrl() + "/Home/DownloadQuestion?p1=" + questionId;
                        ViewBag.DownloadURL = downloadURL1;
                        ViewBag.transactionid = transactionid;

                        try
                        {
                            string strURL = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFilePath1);
                            string strFileName = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFileName1);
                            WebClient req = new WebClient();
                            HttpResponse response = System.Web.HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(videoFileName1) + "\"");
                            //response.AppendHeader("Content-Disposition", "attachment; filename=" + subjectName +" "+ chapterName + " " + questionText + "");
                            byte[] data = req.DownloadData(Server.MapPath(strURL));
                            response.BinaryWrite(data);
                            response.End();
                        }
                        catch (Exception ex)
                        {
                        }

                        return View("ShowVideo", "~/Views/Shared/_IndexPageLayout.cshtml", showVideoViewModel1);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
               


                PayPalManager payPalManager = new PayPalManager();
                var invoice = payPalManager.GetPayPalInvoiceByGUID(transactionid);

                //if (invoice == null || invoice.QuestionID != questionId || invoice.State != "approved")
                //    return RedirectToAction("Index", "Home");

                if (invoice != null && invoice.RequestXML.ToLower().Contains("stripe payment"))
                {
                    //if (invoice.QuestionID != questionId && session_id != invoice.PaymentID)
                    if(invoice.PaymentID == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    //if (invoice == null || invoice.QuestionID != questionId || invoice.State != "approved")
                    if (invoice == null || invoice.State != "approved")
                        return RedirectToAction("Index", "Home");
                }
            }

            List<object> lstRet = new SubjectsController().getChapterContentsQuestionBySubjectID(subID);
            var showVideoViewModel = new ShowVideoViewModel();
            var videoFileName = "";
            var videoFilePath = "";
            var subjectName = "";
            var chapterName = "";
            var questionText = "";

            foreach (var item in lstRet)
            {
                var questionIdDB = item.GetType().GetProperty("ContentsHolder").GetValue(item, null).ToString();

                if (questionId.ToString() == questionIdDB)
                {
                    videoFilePath = item.GetType().GetProperty("VideoFilePath").GetValue(item, null).ToString();
                    videoFileName = item.GetType().GetProperty("VideoFileName").GetValue(item, null).ToString();
                    subjectName = item.GetType().GetProperty("SubjectName").GetValue(item, null).ToString();
                    chapterName = item.GetType().GetProperty("ChapterName").GetValue(item, null).ToString();
                    questionText = item.GetType().GetProperty("QuestionText").GetValue(item, null).ToString();
                }
            }

            ViewBag.Path = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFilePath);


            showVideoViewModel.SubjectName = subjectName;
            showVideoViewModel.ChapterName = chapterName;

            showVideoViewModel.ExerciseName = chapterName;
            showVideoViewModel.QuestionText = questionText;

            ViewBag.ShowVideoPastPaper = 1;

            string downloadURL = GetBaseUrl() + "/Home/DownloadQuestion?p1=" + questionId;
            ViewBag.DownloadURL = downloadURL;
            ViewBag.transactionid = transactionid;

            try
            {
                string strURL = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFilePath);
                string strFileName = "/ContentStorage/ChaptersContents/" + System.IO.Path.GetFileName(videoFileName);
                WebClient req = new WebClient();
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(videoFileName) +  "\"");
                //response.AppendHeader("Content-Disposition", "attachment; filename=" + subjectName +" "+ chapterName + " " + questionText + "");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
            }
            catch (Exception ex)
            {
            }

            return View("ShowVideo", "~/Views/Shared/_IndexPageLayout.cshtml", showVideoViewModel);

        }

        public ActionResult DownloadQuestionPDF(int questionId, string session_id = null)
        {
            

            QuestionManager questionManager = new QuestionManager();
            var question = questionManager.GetQuestionByID(questionId);


            if (question != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {                      
                        var pdfFilePath = System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), question.PdfDocFilePath);
                        var pdfFileName = System.IO.Path.GetFileName(pdfFilePath);
                        ziparchive.CreateEntryFromFile(pdfFilePath, pdfFileName);
                    }

                    return File(memoryStream.ToArray(), "application/zip", "Files.zip");
                }
            }

            return null;
        }
        public ActionResult DownloadQuestion(string p1, string tId, string session_id = null)
        {
            if (string.IsNullOrEmpty(tId))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                PayPalManager payPalManager = new PayPalManager();
                var invoice = payPalManager.GetPayPalInvoiceByGUID(tId);

                //if (invoice == null || invoice.QuestionID != int.Parse(p1) || invoice.State != "approved")
                //    return RedirectToAction("Index", "Home");

                if (invoice != null && invoice.RequestXML.ToLower().Contains("stripe payment"))
                {
                    if (invoice.QuestionID != int.Parse(p1) && session_id != invoice.PaymentID)
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (invoice == null || invoice.QuestionID != int.Parse(p1) || invoice.State != "approved")
                        return RedirectToAction("Index", "Home");
                }
            }

            QuestionManager questionManager = new QuestionManager();
            var question = questionManager.GetQuestionByID(int.Parse(p1));


            if (question != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var videoFilePath = System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), question.VideoFilePath);
                        var videofileName = System.IO.Path.GetFileName(videoFilePath);
                        ziparchive.CreateEntryFromFile(videoFilePath, videofileName);

                        var pdfFilePath = System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), question.PdfDocFilePath);
                        var pdfFileName = System.IO.Path.GetFileName(pdfFilePath);
                        ziparchive.CreateEntryFromFile(pdfFilePath, pdfFileName);

                    }

                    return File(memoryStream.ToArray(), "application/zip", "Files.zip");
                }
            }

            return null;
        }
    }
}