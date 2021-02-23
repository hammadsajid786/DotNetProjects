using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;
using MVCSlider.App_Code;
using System.ServiceModel;
using System.ServiceModel.Web;
using Newtonsoft.Json;

namespace MVCSlider.Controllers
{
    public class ChaptersController : Controller
    {

        public int QueryChapterContentID
        {
            get
            {
                return Session["ChapterContentID"] == null ? 0 : Convert.ToInt32(Session["ChapterContentID"]);
            }

            set
            {
                Session["ChapterContentID"] = value;
            }
        }

        private void verifySessionforAdmin()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["LoginID"])))
            {
                Response.Redirect("~/Home/Index");
            }
            else if (Convert.ToString(Session["UserType"]) != Convert.ToString(ExtensionMethods.UserTypes.Admin))
            {
                Response.Redirect("~/Home/Index");
            }
        }


        public ActionResult Index()
        {
            verifySessionforAdmin();


            List<Subjects> lstSubject = new SubjectsController().getSubjects();
            List<SelectListItem> lstSubjectsDropDown = new List<SelectListItem>();
            var varlstSubject = lstSubject.Select(item => new { item.SubjectName, item.SubjectID });

            lstSubjectsDropDown.Add(new SelectListItem() { Text = "Select Subject", Value = "0" });
            foreach (var item in varlstSubject)
            {
                lstSubjectsDropDown.Add(new SelectListItem() { Text = item.SubjectName, Value = item.SubjectID.ToString() });
            }


            ViewBag.SubjectLookup = lstSubjectsDropDown;
            return View();
        }

        [HttpGet]
        public JsonResult GetChaptersBySubjectID(int SubjectID)
        {
            List<object> lstChapters = new List<object>();
            lstChapters = new SubjectsController().getChaptersBySubjectsID(SubjectID);
            //lstChapters.Add(new { SubjectChaptersID = 1, ChapterCode = "P1-1", ChapterName = "Quadratics", SubjectID = 3 });
            return Json(lstChapters, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveUpdate(string Chapter)
        {
            dynamic clsChapter = JsonConvert.DeserializeObject(Chapter);

            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectChapterID",(clsChapter.SubjectChapterID == "" ? 0 : (int)clsChapter.SubjectChapterID)),
                    new KeyValuePair<string,object>("SubjectID",(clsChapter.SubjectID == "" ? 0 : (int)clsChapter.SubjectID)),
                    new KeyValuePair<string,object>("ChapterCode",Convert.ToString(clsChapter.ChapterCode)),
                    new KeyValuePair<string,object>("ChapterName",Convert.ToString(clsChapter.ChapterName)),
                    new KeyValuePair<string,object>("CreatedBy",(int)Session["UserID"])

                    };

            clsDAC.Execute_Procedure("sp_SubjectsChaptersCRUD", lstData);
            ViewBag.Error = clsDAC.Error_Message;


            List<object> lstChapters = new List<object>();
            lstChapters = new SubjectsController().getChaptersBySubjectsID((int)clsChapter.SubjectID);
            return Json(lstChapters, JsonRequestBehavior.AllowGet);
        }


        public void QuestionPaymentSave(QuestionPayment questionPayment)
        {

            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("subjbectid",questionPayment.subjbectid),
                    new KeyValuePair<string,object>("questionid",questionPayment.questionid),
                    new KeyValuePair<string,object>("transactionid",questionPayment.transactionid),
                    new KeyValuePair<string,object>("variant",questionPayment.variant)


                    };

            clsDAC.Execute_Procedure("sp_PaymentQuestionSave", lstData);
            ViewBag.Error = clsDAC.Error_Message;



        }

        [HttpPost]
        public JsonResult SaveDelete(int SubjectChapterID, int SubjectID)
        {
            //dynamic clsChapter = JsonConvert.DeserializeObject(Chapter);

            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectChapterID",SubjectChapterID),
                    new KeyValuePair<string,object>("CreatedBy",(int)Session["UserID"])

                    };

            clsDAC.Execute_Procedure("sp_SubjectsChaptersDelete", lstData);
            ViewBag.Error = clsDAC.Error_Message;


            List<object> lstChapters = new List<object>();
            lstChapters = new SubjectsController().getChaptersBySubjectsID(SubjectID);
            return Json(lstChapters, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ChapterContents(int ID)
        {
            verifySessionforAdmin();

            if (Request.QueryString["ID"] != null)
            {
                QueryChapterContentID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            if (ID > 0)
            {
                QueryChapterContentID = ID;
            }

            List<object> lstRet = new List<object>();
            lstRet = new SubjectsController().getChapterContentsByID(QueryChapterContentID);
            string ChapterName = "";
            string SubjectName = "";
            string SubjectType = "";

            if (lstRet.Count() > 0)
            {
                var ChapterContentBasic = lstRet.FirstOrDefault();
                ChapterName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("ChapterName").GetValue(ChapterContentBasic));
                SubjectName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectName").GetValue(ChapterContentBasic));
                SubjectType = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectType").GetValue(ChapterContentBasic));
            }

            ViewBag.SubjectsChaptersID = QueryChapterContentID;
            ViewBag.ChapterName = ChapterName;
            ViewBag.SubjectName = SubjectName;
            ViewBag.SubjectType = SubjectType;
            if (lstRet != null && lstRet.Count == 1)
            {
                var ChapterContentBasic = lstRet.FirstOrDefault();
                var ContentsHolder = Convert.ToString(ChapterContentBasic.GetType().GetProperty("ContentsHolder").GetValue(ChapterContentBasic));

                if (ContentsHolder == "0")
                {
                    lstRet.RemoveAt(0);
                }

            }


            ViewBag.ChapterContentsList = lstRet;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ChapterContents(FormCollection frmCollection, int ID = 0, int page = 1)
        {
            verifySessionforAdmin();
            var ImageFileName = ""; //using this field to save variant value
            var VideoFileName = "";
            var PdfFileName = "";

            var ImageFilePath = "";
            var VideoFilePath = "";
            var PdfFilePath = "";
            var isfree = false;
            var PaperYear = "";



            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();
            List<string> FilesAlreadyLoaded = new List<string>();
            int SubjectChapterID = 0;
            isfree = Convert.ToBoolean(frmCollection["Isfree"]);
            Int32.TryParse(Convert.ToString(frmCollection["SubjectChapterID"]), out SubjectChapterID);

            if (!string.IsNullOrEmpty(frmCollection["PaperYear"]))
            {
                PaperYear = Convert.ToString(frmCollection["PaperYear"]);
            }
            if (!string.IsNullOrEmpty(frmCollection["QuestionDiagram"]))
            {
                ImageFileName = Convert.ToString(frmCollection["QuestionDiagram"]);
            }
            if (Request.Files.Count > 0)
            {
                for (int updFile = 0; updFile < Request.Files.Count; updFile++)
                {
                    //string ContentFileName = Convert.ToString(dt_1.Rows[0]["ContentFileName"]);
                    var supportedFile = false;
                    HttpPostedFileBase postedFile = Request.Files[updFile];
                    var fileName = postedFile.FileName;
                    var fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
                    System.Guid Guid = System.Guid.NewGuid();
                    var guidStr = Guid.ToString() + fileExtension;

                    if (fileExtension == ".mp4")
                    {
                        supportedFile = true;
                        VideoFileName = fileName;
                        VideoFilePath = guidStr;
                    }
                    //else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                    //{
                    //    supportedFile = true;
                    //    ImageFileName = fileName;
                    //    ImageFilePath = guidStr;
                    //}
                    else if (fileExtension == ".pdf")
                    {
                        supportedFile = true;
                        PdfFileName = fileName;
                        PdfFilePath = guidStr;
                    }

                    if (supportedFile)
                    {
                        fileName = guidStr; //System.IO.Path.GetFileName(fileName);
                        System.IO.MemoryStream mem = new System.IO.MemoryStream();
                        var path = System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), fileName);
                        //    postedFile.InputStream.CopyTo(mem);
                        //    System.IO.File.WriteAllBytes(System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), fileName), mem.ToArray());
                        //
                        postedFile.SaveAs(path);
                    }
                }
            }


            {
                List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                {
                new KeyValuePair<string,object>("SubjectsChaptersID",SubjectChapterID),
                new KeyValuePair<string,object>("Url",frmCollection["youTubeUrl"]),
                new KeyValuePair<string,object>("CreatedBy",Session["UserID"]),
                new KeyValuePair<string,object>("QuestionText",frmCollection["questionText"]),
               new KeyValuePair<string,object>("ImageFileName",ImageFileName),
                new KeyValuePair<string,object>("VideoFileName",VideoFileName),
                new KeyValuePair<string,object>("PaperYear",PaperYear),
                new KeyValuePair<string,object>("PdfDocFile",PdfFileName),
                new KeyValuePair<string,object>("ImageFilePath",ImageFilePath),
                new KeyValuePair<string,object>("VideoFilePath",VideoFilePath),
                new KeyValuePair<string,object>("PdfDocFilePath",PdfFilePath),
                new KeyValuePair<string,object>("Isfree",isfree),
                };

                DataTable dt_1 = clsDAC.Read_DT_Procedure("sp_SaveChaptersQuestions", lstData);
                ViewBag.Error = clsDAC.Error_Message;
            }
            //if (Request.Files.Count > 0)
            //{
            //    for (int updFile = 0; updFile < Request.Files.Count; updFile++)
            //    {
            //        List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
            //        {
            //        new KeyValuePair<string,object>("SubjectsChaptersID",SubjectChapterID),
            //        new KeyValuePair<string,object>("OriginalFileName",Request.Files[updFile].FileName),
            //        new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
            //        };

            //        DataTable dt_1 = clsDAC.Read_DT_Procedure("sp_SaveChaptersContents", lstData);
            //        ViewBag.Error = clsDAC.Error_Message;

            //        if (dt_1 != null || dt_1.Rows.Count > 0)
            //        {
            //            if (Convert.ToBoolean(dt_1.Rows[0]["IsAlreadyLoaded"]))
            //            {
            //                FilesAlreadyLoaded.Add(Convert.ToString(dt_1.Rows[0]["OriginalFileName"]));
            //            }
            //            else
            //            {
            //                string ContentFileName = Convert.ToString(dt_1.Rows[0]["ContentFileName"]);
            //                HttpPostedFileBase postedFile = Request.Files[updFile];
            //                System.IO.MemoryStream mem = new System.IO.MemoryStream();
            //                postedFile.InputStream.CopyTo(mem);
            //                System.IO.File.WriteAllBytes(Server.MapPath("/ContentStorage/ChaptersContents/") + ContentFileName, mem.ToArray());
            //            }
            //        }
            //    }
            //}


            List<object> lstRet = new List<object>();
            lstRet = new SubjectsController().getChapterContentsByID(SubjectChapterID);
            string ChapterName = "";
            string SubjectName = "";

            if (lstRet.Count() > 0)
            {
                var ChapterContentBasic = lstRet.FirstOrDefault();
                ChapterName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("ChapterName").GetValue(ChapterContentBasic));
                SubjectName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectName").GetValue(ChapterContentBasic));
            }

            ViewBag.SubjectsChaptersID = SubjectChapterID;
            ViewBag.ChapterName = ChapterName;
            ViewBag.SubjectName = SubjectName;
            ViewBag.ChapterContentsList = lstRet;
            ViewBag.FilesAlreadyLoaded = FilesAlreadyLoaded;
            ViewBag.SubjectsChaptersID = ID;

            //////////Response.Redirect(string.Format("~/Chapters/ChapterContents?ID={0}", SubjectChapterID));
            ////RedirectToAction(string.Format("ChapterContents?ID={0}", SubjectChapterID));
            //   return View(SubjectChapterID);
            return RedirectToAction("ChapterContents", new { ID = SubjectChapterID });
        }





        // GET: Subjects/Delete/5
        public ActionResult ChapterContentDelete(int CHID, int SCID)
        {
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectsChaptersID",SCID),
                    new KeyValuePair<string,object>("ContentHolder",CHID),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

            clsDAC.Execute_Procedure("sp_DeleteSubjectChapterContent", lstData);

            ViewBag.Error = clsDAC.Error_Message;

            if (clsDAC.Error_Message.Trim() == "")
            {
                Response.Redirect(string.Format("~/Chapters/ChapterContents?ID={0}", SCID));
            }
            else
            {
                Response.Redirect(string.Format("~/Chapters/ChapterContents?ID={0}&removed=0", SCID));
            }

            return View();
        }

        public ActionResult ChapterQuetionsDelete(int CHID, int SCID)
        {
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectsChaptersID",SCID),
                    new KeyValuePair<string,object>("ContentHolder",CHID),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

            clsDAC.Execute_Procedure("sp_DeleteSubjectQuestions", lstData);

            ViewBag.Error = clsDAC.Error_Message;

            if (clsDAC.Error_Message.Trim() == "")
            {
                Response.Redirect(string.Format("~/Chapters/ChapterContents?ID={0}", SCID));
            }
            else
            {
                Response.Redirect(string.Format("~/Chapters/ChapterContents?ID={0}&removed=0", SCID));
            }

            return View();
        }
        [HttpGet]
        public ActionResult ChapterQuetionsEdit(int CHID, int SCID)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            Questions question = new Questions();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsChapterQuestionsByQuestionID", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("QuestionID", CHID) });


            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    question.QuestionID = Convert.ToInt32(dr_1["QuestionID"]);
                    question.URL = dr_1["URL"] == DBNull.Value ? "" : Convert.ToString(dr_1["URL"]);
                    question.SubjectsChaptersID = (dr_1["SubjectsChaptersID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectsChaptersID"]));
                    question.QuestionText = dr_1["QuestionText"] == DBNull.Value ? "" : Convert.ToString(dr_1["QuestionText"]);
                    question.ImageFileName = dr_1["ImageFileName"] == DBNull.Value ? "" : Convert.ToString(dr_1["ImageFileName"]);
                    question.VideoFileName = dr_1["VideoFileName"] == DBNull.Value ? "" : Convert.ToString(dr_1["VideoFileName"]);
                    question.PaperYear = dr_1["PaperYear"] == DBNull.Value ? "" : Convert.ToString(dr_1["PaperYear"]);
                    question.ImageFilePath = dr_1["ImageFilePath"] == DBNull.Value ? "" : Convert.ToString(dr_1["ImageFilePath"]);
                    question.PdfDocFile = dr_1["PdfDocFile"] == DBNull.Value ? "" : Convert.ToString(dr_1["PdfDocFile"]);
                    question.PdfDocFilePath = dr_1["PdfDocFilePath"] == DBNull.Value ? "" : Convert.ToString(dr_1["PdfDocFilePath"]);
                    question.VideoFilePath = dr_1["VideoFilePath"] == DBNull.Value ? "" : Convert.ToString(dr_1["VideoFilePath"]);
                    question.isfree = Convert.ToBoolean(dr_1["Isfree"]);
                }
            }

            List<object> lstRet = new List<object>();
            lstRet = new SubjectsController().getChapterContentsByID(SCID);
            string ChapterName = "";
            string SubjectName = "";
            string SubjectType = "";
            if (lstRet.Count() > 0)
            {
                var ChapterContentBasic = lstRet.FirstOrDefault();
                ChapterName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("ChapterName").GetValue(ChapterContentBasic));
                SubjectName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectName").GetValue(ChapterContentBasic));
                SubjectType = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectType").GetValue(ChapterContentBasic));
            }

            ViewBag.SubjectsChaptersID = SCID;
            ViewBag.ChapterName = ChapterName;
            ViewBag.SubjectName = SubjectName;
            ViewBag.SubjectType = SubjectType;

           
            return View(question);
        }

        [HttpPost]
        public ActionResult ChapterQuetionsEdit(FormCollection frmCollection)
        {
            var ImageFileName = ""; //using this field to save variant value
            var VideoFileName = "";
            var PdfFileName = "";

            var ImageFilePath = "";
            var VideoFilePath = "";
            var PdfFilePath = "";
            var isfree = false;
            var PaperYear = "";



            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();
            List<string> FilesAlreadyLoaded = new List<string>();
            int SubjectChapterID = 0;
            int QuestionID = 0;
            isfree = Convert.ToBoolean(frmCollection["Isfree"]);
            Int32.TryParse(Convert.ToString(frmCollection["SubjectChapterID"]), out SubjectChapterID);
            Int32.TryParse(Convert.ToString(frmCollection["QuestionID"]), out QuestionID);
            if (!string.IsNullOrEmpty(frmCollection["PaperYear"]))
            {
                PaperYear = Convert.ToString(frmCollection["PaperYear"]);
            }
            if (!string.IsNullOrEmpty(frmCollection["QuestionDiagram"]))
            {
                ImageFileName = Convert.ToString(frmCollection["QuestionDiagram"]);
            }
            if (Request.Files.Count > 0)
            {
                for (int updFile = 0; updFile < Request.Files.Count; updFile++)
                {
                    //string ContentFileName = Convert.ToString(dt_1.Rows[0]["ContentFileName"]);
                    var supportedFile = false;
                    HttpPostedFileBase postedFile = Request.Files[updFile];
                    var fileName = postedFile.FileName;
                    var fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
                    System.Guid Guid = System.Guid.NewGuid();
                    var guidStr = Guid.ToString() + fileExtension;

                    if (fileExtension == ".mp4")
                    {
                        supportedFile = true;
                        VideoFileName = fileName;
                        VideoFilePath = guidStr;
                    }
                    //else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                    //{
                    //    supportedFile = true;
                    //    ImageFileName = fileName;
                    //    ImageFilePath = guidStr;
                    //}
                    else if (fileExtension == ".pdf")
                    {
                        supportedFile = true;
                        PdfFileName = fileName;
                        PdfFilePath = guidStr;
                    }

                    if (supportedFile)
                    {
                        fileName = guidStr; //System.IO.Path.GetFileName(fileName);
                        System.IO.MemoryStream mem = new System.IO.MemoryStream();
                        var path = System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), fileName);
                        //    postedFile.InputStream.CopyTo(mem);
                        //    System.IO.File.WriteAllBytes(System.IO.Path.Combine(Server.MapPath("~/ContentStorage/ChaptersContents/"), fileName), mem.ToArray());
                        //
                        postedFile.SaveAs(path);
                    }
                }
            }


            {
                List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string,object>("QuestionID",QuestionID),
                new KeyValuePair<string,object>("SubjectsChaptersID",SubjectChapterID),
                new KeyValuePair<string,object>("Url",frmCollection["youTubeUrl"]),
                new KeyValuePair<string,object>("CreatedBy",Session["UserID"]),
                new KeyValuePair<string,object>("QuestionText",frmCollection["questionText"]),
               new KeyValuePair<string,object>("ImageFileName",ImageFileName),
                new KeyValuePair<string,object>("VideoFileName",VideoFileName),
                new KeyValuePair<string,object>("PaperYear",PaperYear),
                new KeyValuePair<string,object>("PdfDocFile",PdfFileName),
                new KeyValuePair<string,object>("ImageFilePath",ImageFilePath),
                new KeyValuePair<string,object>("VideoFilePath",VideoFilePath),
                new KeyValuePair<string,object>("PdfDocFilePath",PdfFilePath),
                new KeyValuePair<string,object>("Isfree",isfree),
                };

                DataTable dt_1 = clsDAC.Read_DT_Procedure("sp_SaveChaptersQuestions", lstData);
                ViewBag.Error = clsDAC.Error_Message;
            }
            return View();
        }

        public dynamic getChaptersQuesionsByQuestionID(int QuestionID)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<Questions> lstRet = new List<Questions>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsChapterQuestionsByQuestionID", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("QuestionID", QuestionID) });


            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new Questions
                    {
                        QuestionID = Convert.ToInt32(dr_1["QuestionID"]),
                        URL = dr_1["URL"] == DBNull.Value ? "" : Convert.ToString(dr_1["URL"]),
                        SubjectsChaptersID = (dr_1["SubjectsChaptersID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectsChaptersID"])),
                        QuestionText = dr_1["QuestionText"] == DBNull.Value ? "" : Convert.ToString(dr_1["QuestionText"]),
                        ImageFileName = dr_1["ImageFileName"] == DBNull.Value ? "" : Convert.ToString(dr_1["ImageFileName"]),
                        VideoFileName = dr_1["VideoFileName"] == DBNull.Value ? "" : Convert.ToString(dr_1["VideoFileName"]),
                        PaperYear = dr_1["PaperYear"] == DBNull.Value ? "" : Convert.ToString(dr_1["PaperYear"]),
                        ImageFilePath = dr_1["ImageFilePath"] == DBNull.Value ? "" : Convert.ToString(dr_1["ImageFilePath"]),
                        PdfDocFile = dr_1["PdfDocFile"] == DBNull.Value ? "" : Convert.ToString(dr_1["PdfDocFile"]),
                        PdfDocFilePath = dr_1["PdfDocFilePath"] == DBNull.Value ? "" : Convert.ToString(dr_1["PdfDocFilePath"]),
                        VideoFilePath = dr_1["VideoFilePath"] == DBNull.Value ? "" : Convert.ToString(dr_1["VideoFilePath"]),
                        isfree = Convert.ToBoolean(dr_1["Isfree"])
                    });
                }
            }



            return lstRet;
        }
        public ActionResult Exercises(int ID)
        {
            verifySessionforAdmin();

            List<object> lstRet = new List<object>();
            lstRet = getExercisesByChapterID(ID);
            string ChapterName = "";
            string SubjectName = "";

            if (lstRet.Count() > 0)
            {
                var ChapterContentBasic = lstRet.FirstOrDefault();
                ChapterName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("ChapterName").GetValue(ChapterContentBasic));
                SubjectName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectName").GetValue(ChapterContentBasic));
            }

            ViewBag.ParentChapterID = ID;
            ViewBag.ChapterName = ChapterName;
            ViewBag.SubjectName = SubjectName;
            ViewBag.ExercisesList = lstRet;

            return View();
        }


        [HttpPost]
        public ActionResult Exercises(FormCollection frmCollection)
        {
            verifySessionforAdmin();

            int SubjectsChapterID = 0;
            string Code = "";
            string ExerciseName = "";
            int ParentChapterID = 0;

            if (string.IsNullOrEmpty(frmCollection["SubjectsChaptersID"]))
            {
                SubjectsChapterID = 0;
            }
            else
            {
                SubjectsChapterID = Convert.ToInt32(frmCollection["SubjectsChaptersID"]);
            }

            Code = Convert.ToString(frmCollection["ExerciseCode"]);
            ExerciseName = Convert.ToString(frmCollection["ExerciseName"]);
            ParentChapterID = Convert.ToInt32(frmCollection["ParentChapterID"]);

            MSSQL_DAC clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectsChaptersID",SubjectsChapterID),
                    new KeyValuePair<string,object>("ExerciseCode",Code),
                    new KeyValuePair<string,object>("ExerciseName",ExerciseName),
                    new KeyValuePair<string,object>("ParentChapterID",ParentChapterID),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

            clsDAC.Execute_Procedure("sp_ExerciseCRUD", lstData);
            ViewBag.Error = clsDAC.Error_Message;


            List<object> lstRet = new List<object>();
            lstRet = getExercisesByChapterID(ParentChapterID);
            string ChapterName = "";
            string SubjectName = "";

            if (lstRet.Count() > 0)
            {
                var ChapterContentBasic = lstRet.FirstOrDefault();
                ChapterName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("ChapterName").GetValue(ChapterContentBasic));
                SubjectName = Convert.ToString(ChapterContentBasic.GetType().GetProperty("SubjectName").GetValue(ChapterContentBasic));
            }

            ViewBag.ParentChapterID = ParentChapterID;
            ViewBag.ChapterName = ChapterName;
            ViewBag.SubjectName = SubjectName;
            ViewBag.ExercisesList = lstRet;
            return View();
        }

        public ActionResult ExercisesDelete(int ID, int ParentID)
        {

            MSSQL_DAC clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectsChaptersID",ID),
                    new KeyValuePair<string,object>("Mode","Delete"),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

            clsDAC.Execute_Procedure("sp_ExerciseCRUD", lstData);
            ViewBag.Error = clsDAC.Error_Message;

            Response.Redirect("~/Chapters/Exercises?ID=" + ParentID);

            return View();
        }


        public List<object> getExercisesByChapterID(int SubjectsChaptersID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();



            dt_1 = clsDAC.Read_DT_Procedure("sp_GetExercises", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectsChaptersID", SubjectsChaptersID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {
                        SubjectsChaptersID = (dr_1["SubjectsChaptersID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectsChaptersID"])),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        ExerciseCode = Convert.ToString(dr_1["ExerciseCode"]),
                        ExerciseName = Convert.ToString(dr_1["ExerciseName"]),
                        ParentChapterID = (dr_1["ParentChapterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["ParentChapterID"])),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"])
                    });
                }
            }

            return lstRet;
        }

    }
}