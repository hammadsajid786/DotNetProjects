using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;
using MVCSlider.App_Code;
using System.Web.DynamicData;

namespace MVCSlider.Controllers
{
    public class SubjectsController : Controller
    {

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


        public ActionResult List()
        {
            verifySessionforAdmin();

            List<Subjects> modelSubjectsList = getSubjects();
            return View(modelSubjectsList);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            verifySessionforAdmin();
            Subjects modelSubjects = new Subjects();
            IEnumerable<SelectListItem> ddlSubjectType = null;
            IEnumerable<SelectListItem> ddlparentSubjects = null;
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            DataTable dt_1 = clsDAC.Read_DT_Query("SELECT ValueColumn,DisplayColumn FROM dbo.tblLookups WHERE LookupCode='SubjectType'");
            if (dt_1 != null)
            {
                ddlSubjectType = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["ValueColumn"]), Text = Convert.ToString(x["DisplayColumn"]) });
            }

            dt_1 = clsDAC.Read_DT_Query("SELECT SubjectID, SubjectName FROM dbo.tblSubjects WHERE SubjectType <> 8 AND ISNULL(IsActive,1) = 1");
            if (dt_1 != null)
            {
                ddlparentSubjects = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["SubjectID"]), Text = Convert.ToString(x["SubjectName"]) });
            }


            ViewBag.MainModel = modelSubjects;
            ViewBag.ddSubjectTypelist = ddlSubjectType;
            ViewBag.ddlparentSubjectslist = ddlparentSubjects;
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Subjects modelSubject = null;
            IEnumerable<SelectListItem> ddlSubjectType = null;
            IEnumerable<SelectListItem> ddlparentSubjects = null;
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            DataTable dt_1 = clsDAC.Read_DT_Query("SELECT ValueColumn,DisplayColumn FROM dbo.tblLookups WHERE LookupCode='SubjectType'");
            if (dt_1 != null)
            {
                ddlSubjectType = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["ValueColumn"]), Text = Convert.ToString(x["DisplayColumn"]) });
            }

            dt_1 = clsDAC.Read_DT_Query("SELECT SubjectID, SubjectName FROM dbo.tblSubjects WHERE SubjectType <> 8 AND ISNULL(IsActive,1) = 1");
            if (dt_1 != null)
            {
                ddlparentSubjects = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["SubjectID"]), Text = Convert.ToString(x["SubjectName"]) });
                ddlparentSubjects.ToList().Insert(0, new SelectListItem() { Value = "0", Text = "Select Subject" });
            }


            try
            {

                modelSubject = new Subjects()
                {
                    SubjectCode = collection["SubjectCode"],
                    SubjectName = collection["SubjectName"],
                    SubjectType = Convert.ToInt32(collection["modelSubject.SubjectType"]),
                    SubjectYear = collection["SubjectYear"],
                    ParentSubjectID = (string.IsNullOrEmpty(collection["modelSubject.ParentSubjectID"]) ? 0 : Convert.ToInt32(collection["modelSubject.ParentSubjectID"]))
                };

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase postedFile = Request.Files[0];
                    System.IO.MemoryStream mem = new System.IO.MemoryStream();
                    postedFile.InputStream.CopyTo(mem);
                    modelSubject.SubjectImage = mem.ToArray();
                }

                clsDAC = new MSSQL_DAC();

                List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectName",modelSubject.SubjectName),
                    new KeyValuePair<string,object>("SubjectCode",modelSubject.SubjectCode),
                    new KeyValuePair<string,object>("SubjectType",modelSubject.SubjectType),
                    new KeyValuePair<string,object>("SubjectYear",modelSubject.SubjectYear),
                    new KeyValuePair<string,object>("ParentSubjectID",modelSubject.ParentSubjectID),
                    new KeyValuePair<string,object>("SubjectImage",modelSubject.SubjectImage),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

                clsDAC.Execute_Procedure("sp_CreateSubjects", lstData);

                ViewBag.Error = clsDAC.Error_Message;

                if (clsDAC.Error_Message.Trim() == "")
                {
                    Response.Redirect("~/Subjects/List");
                }

                ViewBag.MainModel = modelSubject;
                ViewBag.ddSubjectTypelist = ddlSubjectType;
                ViewBag.ddlparentSubjectslist = ddlparentSubjects;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            ViewBag.MainModel = modelSubject;
            ViewBag.ddSubjectTypelist = ddlSubjectType;
            ViewBag.ddlparentSubjectslist = ddlparentSubjects;
            return View();
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int id)
        {
            verifySessionforAdmin();

            Subjects modelSubjects = new Subjects();
            IEnumerable<SelectListItem> ddlSubjectType = null;
            List<SelectListItem> slstSubjectTypeRet = new List<SelectListItem>();
            IEnumerable<SelectListItem> ddlparentSubjects = null;
            List<SelectListItem> slstParentSubjectRet = new List<SelectListItem>();
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            DataTable dt_1 = clsDAC.Read_DT_Query("SELECT ValueColumn,DisplayColumn FROM dbo.tblLookups WHERE LookupCode='SubjectType'");
            if (dt_1 != null)
            {
                ddlSubjectType = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["ValueColumn"]), Text = Convert.ToString(x["DisplayColumn"]) });
            }

            dt_1 = clsDAC.Read_DT_Query("SELECT SubjectID, SubjectName FROM dbo.tblSubjects WHERE SubjectType <> 8 AND ISNULL(IsActive,1) = 1");
            if (dt_1 != null)
            {
                ddlparentSubjects = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["SubjectID"]), Text = Convert.ToString(x["SubjectName"]) });
            }


            if (id != 0)
            {
                modelSubjects = getSubjectByID(id);
                foreach (var item in ddlSubjectType)
                {
                    if (item.Value == Convert.ToString(modelSubjects.SubjectType))
                    {
                        item.Selected = true;
                    }
                    slstSubjectTypeRet.Add(item);
                }


                foreach (var item in ddlparentSubjects)
                {
                    if (item.Value == Convert.ToString(modelSubjects.ParentSubjectID))
                    {
                        item.Selected = true;
                    }
                    slstParentSubjectRet.Add(item);
                }
            }

            ViewBag.MainModel = modelSubjects;
            ViewBag.ddSubjectTypelist = slstSubjectTypeRet;
            ViewBag.ddlparentSubjectslist = slstParentSubjectRet;
            return View();
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Subjects modelSubject = null;
            IEnumerable<SelectListItem> ddlSubjectType = null;
            IEnumerable<SelectListItem> ddlparentSubjects = null;
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            DataTable dt_1 = clsDAC.Read_DT_Query("SELECT ValueColumn,DisplayColumn FROM dbo.tblLookups WHERE LookupCode='SubjectType'");
            if (dt_1 != null)
            {
                ddlSubjectType = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["ValueColumn"]), Text = Convert.ToString(x["DisplayColumn"]) });
            }

            dt_1 = clsDAC.Read_DT_Query("SELECT SubjectID, SubjectName FROM dbo.tblSubjects WHERE SubjectType <> 8 AND ISNULL(IsActive,1) = 1");
            if (dt_1 != null)
            {
                ddlparentSubjects = dt_1.Rows.Cast<DataRow>().ToList().Select(x => new SelectListItem() { Value = Convert.ToString(x["SubjectID"]), Text = Convert.ToString(x["SubjectName"]) });
            }

            try
            {
                modelSubject = new Subjects()
                {
                    SubjectID = id,
                    SubjectCode = collection["SubjectCode"],
                    SubjectName = collection["SubjectName"],
                    SubjectType = Convert.ToInt32(collection["modelSubject.SubjectType"]),
                    SubjectYear = collection["SubjectYear"],
                    ParentSubjectID = (collection["modelSubject.ParentSubjectID"].Trim() == "" ? 0 : Convert.ToInt32(collection["modelSubject.ParentSubjectID"]))
                };

                if (!string.IsNullOrEmpty(collection["SubjectImage"]))
                {
                    modelSubject.SubjectImage = Convert.FromBase64String(Convert.ToString(collection["SubjectImage"]));
                }


                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase postedFile = Request.Files[0];
                    System.IO.MemoryStream mem = new System.IO.MemoryStream();
                    postedFile.InputStream.CopyTo(mem);
                    if (mem.Length > 0)
                    {
                        modelSubject.SubjectImage = mem.ToArray();
                    }
                }


                clsDAC = new MSSQL_DAC();

                List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectID",modelSubject.SubjectID),
                    new KeyValuePair<string,object>("SubjectName",modelSubject.SubjectName),
                    new KeyValuePair<string,object>("SubjectCode",modelSubject.SubjectCode),
                    new KeyValuePair<string,object>("SubjectType",modelSubject.SubjectType),
                    new KeyValuePair<string,object>("SubjectYear",modelSubject.SubjectYear),
                    new KeyValuePair<string,object>("ParentSubjectID",modelSubject.ParentSubjectID),
                    new KeyValuePair<string,object>("SubjectImage",modelSubject.SubjectImage),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

                clsDAC.Execute_Procedure("sp_UpdateSubjects", lstData);

                ViewBag.Error = clsDAC.Error_Message;

                if (clsDAC.Error_Message.Trim() == "")
                {
                    Response.Redirect("~/Subjects/List");
                }

                ViewBag.MainModel = modelSubject;
                ViewBag.ddSubjectTypelist = ddlSubjectType;
                ViewBag.ddlparentSubjectslist = ddlparentSubjects;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.MainModel = modelSubject;
                ViewBag.ddSubjectTypelist = ddlSubjectType;
                ViewBag.ddlparentSubjectslist = ddlparentSubjects;
                return View();
            }


        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int id)
        {
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("SubjectID",id),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"])
                    };

            clsDAC.Execute_Procedure("sp_DeleteSubjects", lstData);

            ViewBag.Error = clsDAC.Error_Message;

            if (clsDAC.Error_Message.Trim() == "")
            {
                Response.Redirect("~/Subjects/List?removed=1");
            }
            else
            {
                Response.Redirect("~/Subjects/List?removed=0");
            }

            return View();
        }

        public void DeleteInvoiceFromPaymentQuestion(string TID)
        {
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("TID",TID)
                 
                    };

            clsDAC.Execute_Procedure("sp_DeletePaymentQuestionAgaintsTid", lstData);

            ViewBag.Error = clsDAC.Error_Message;
        }

        //---Getting subjects
        public List<Subjects> getSubjects()
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjects");
            List<Subjects> lstSubjects = new List<Subjects>();
            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstSubjects.Add(new Subjects()
                    {
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        SubjectCode = Convert.ToString(dr_1["SubjectCode"]),
                        SubjectImage = (Byte[])(dr_1["SubjectImage"]),
                        SubjectType = Convert.ToInt32(dr_1["SubjectType"]),
                        SubjectTypeDesc = Convert.ToString(dr_1["SubjectTypeDesc"]),
                        SubjectYear = Convert.ToString(dr_1["SubjectYear"]),
                        ParentSubjectID = (dr_1["ParentSubjectID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["ParentSubjectID"]))
                    });
                }
            }

            return lstSubjects;
        }


        public List<Subjects> getSubjectTopics()
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectTopics");
            List<Subjects> lstSubjects = new List<Subjects>();
            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstSubjects.Add(new Subjects()
                    {
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        SubjectCode = Convert.ToString(dr_1["SubjectCode"]),
                        SubjectImage = (Byte[])(dr_1["SubjectImage"]),
                        SubjectType = Convert.ToInt32(dr_1["SubjectType"]),
                        SubjectTypeDesc = Convert.ToString(dr_1["SubjectTypeDesc"]),
                        SubjectYear = Convert.ToString(dr_1["SubjectYear"]),
                        ParentSubjectID = (dr_1["ParentSubjectID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["ParentSubjectID"]))
                    });
                }
            }

            return lstSubjects;
        }
        
        //---Getting subjects by ID
        public Subjects getSubjectByID(int SubjectID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            Subjects clsSubjectRet = null;

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjects", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                DataRow dr_1 = dt_1.Rows[0];

                clsSubjectRet = new Subjects()
                {
                    SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                    SubjectName = Convert.ToString(dr_1["SubjectName"]),
                    SubjectCode = Convert.ToString(dr_1["SubjectCode"]),
                    SubjectImage = (Byte[])(dr_1["SubjectImage"]),
                    SubjectType = Convert.ToInt32(dr_1["SubjectType"]),
                    SubjectTypeDesc = Convert.ToString(dr_1["SubjectTypeDesc"]),
                    SubjectYear = Convert.ToString(dr_1["SubjectYear"]),
                    ParentSubjectID = (dr_1["ParentSubjectID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["ParentSubjectID"]))
                };
            }
            return clsSubjectRet;
        }


        public dynamic getChaptersBySubjectsID(int SubjectID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsChapters", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {
                        SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                    });
                }
            }

            return lstRet;
        }


        public List<SubjectChapters> getChaptersExerciseBySubjectsID(int SubjectID)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<SubjectChapters> lstRet = new List<SubjectChapters>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsChaptersExercises", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID) });


            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new SubjectChapters
                    {
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        SubjectsChaptersID = (dr_1["SubjectsChaptersID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectsChaptersID"])),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectType= Convert.ToInt32(dr_1["SubjectType"]),
                        ChapterType = (dr_1["ChapterType"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["ChapterType"])),
                        ParentChapterID = (dr_1["ParentChapterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["ParentChapterID"]))
                    });
                }
            }



            return lstRet;
        }

        public List<Questions> getChaptersQuesionsBySubjectsID(int SubjectID)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<Questions> lstRet = new List<Questions>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsChapterQuestions", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID) });


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
                        isfree = Convert.ToBoolean(dr_1["Isfree"]),
                        subID = (dr_1["SubjectID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectID"])),


                    }) ;
                }
            }



            return lstRet;
        }

        public List<Questions> getChaptersQuesionsBySubjectsIDAndVariant(int SubjectID, string variant)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<Questions> lstRet = new List<Questions>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsChapterQuestionsByQuestionIDAndVariant", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID), new KeyValuePair<string, object>("variant", variant) });


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
                        isfree = Convert.ToBoolean(dr_1["Isfree"]),
                         subID = (dr_1["SubjectID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectID"])),

                    });
                }
            }



            return lstRet;
        }

        public List<QuestionPayment> getQuesionsFromPaymentQuestion_BySubjectsIDAndTransaction(int SubjectID, string transactionid)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<QuestionPayment> lstRet = new List<QuestionPayment>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsAndQuestionFromQuestionPaymentBySubIdAndTransactionID", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID), new KeyValuePair<string, object>("TransactionId", transactionid) });


            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new QuestionPayment
                    {
                        questionid = Convert.ToInt32(dr_1["questionid"]),
                        subjbectid = Convert.ToInt32(dr_1["subjbectid"]),
                        transactionid = dr_1["transactionid"] == DBNull.Value ? "" : Convert.ToString(dr_1["transactionid"]),
                        variant = dr_1["variant"] == DBNull.Value ? "" : Convert.ToString(dr_1["variant"]),
                        create_date = Convert.ToDateTime(dr_1["create_date"])
                    });
                }
            }



            return lstRet;
        }
        public List<QuestionPayment> getQuesionsFromPaymentQuestion_BySubjectsIDAndTransactionAndVarient(int SubjectID, string transactionid, string variant)
        {
            DataTable dt_1 = null;

            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<QuestionPayment> lstRet = new List<QuestionPayment>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetSubjectsAndQuestionFromQuestionPaymentBySubIdAndTransactionIDandVariantID", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID), new KeyValuePair<string, object>("TransactionId", transactionid), new KeyValuePair<string, object>("Variant", variant) });


            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new QuestionPayment
                    {
                        questionid = Convert.ToInt32(dr_1["questionid"]),
                        subjbectid = Convert.ToInt32(dr_1["subjbectid"]),
                        transactionid = dr_1["transactionid"] == DBNull.Value ? "" : Convert.ToString(dr_1["transactionid"]),
                        variant = dr_1["variant"] == DBNull.Value ? "" : Convert.ToString(dr_1["variant"]),
                        create_date=  Convert.ToDateTime(dr_1["create_date"])

                    });
                }
            }



            return lstRet;
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
        public List<object> getChapterContentsByID(int SubjectsChaptersID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetChaptersContents_1", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectsChaptersID", SubjectsChaptersID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {
                        //RowNumber = Convert.ToInt32(dr_1["RowNumber"]),
                        //ContentsHolder = Convert.ToInt32(dr_1["ContentsHolder"]),
                        //SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        //ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        //ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        //SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        //SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        //ContentFileName = Convert.ToString(dr_1["ContentFileName"]),
                        //OriginalFileName = Convert.ToString(dr_1["OriginalFileName"]),
                        //URL = Convert.ToString(dr_1["URL"]),
                        //QuestionText = Convert.ToString(dr_1["QuestionText"]),

                        RowNumber = Convert.ToInt32(dr_1["RowNumber"]),
                        ContentsHolder = Convert.ToInt32(dr_1["ContentsHolder"]),
                        SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        ImageFileName = Convert.ToString(dr_1["ImageFileName"]),
                        VideoFileName = Convert.ToString(dr_1["VideoFileName"]),
                        PdfDocFile = Convert.ToString(dr_1["PdfDocFile"]),
                        // OriginalFileName = Convert.ToString(dr_1["OriginalFileName"]),
                        URL = Convert.ToString(dr_1["URL"]),
                        QuestionText = Convert.ToString(dr_1["QuestionText"]),
                        SubjectType = Convert.ToString(dr_1["SubjectType"]),
                        ImageFilePath = Convert.ToString(dr_1["ImageFilePath"]),
                        VideoFilePath = Convert.ToString(dr_1["VideoFilePath"]),
                        PdfDocFilePath = Convert.ToString(dr_1["PdfDocFilePath"]),
                        PaperYear = Convert.ToString(dr_1["PaperYear"])
                    });
                    ViewBag.SubjectsChaptersID = SubjectsChaptersID;
                }
            }

            return lstRet;
        }

        public List<object> getChapterContentsQuestionBySubjectID(int SubjectID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetChaptersContents_BySubjectId", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {
                        

                        RowNumber = Convert.ToInt32(dr_1["RowNumber"]),
                        ContentsHolder = Convert.ToInt32(dr_1["ContentsHolder"]),
                        SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        ImageFileName = Convert.ToString(dr_1["ImageFileName"]),
                        VideoFileName = Convert.ToString(dr_1["VideoFileName"]),
                        // OriginalFileName = Convert.ToString(dr_1["OriginalFileName"]),
                        URL = Convert.ToString(dr_1["URL"]),
                       QuestionText = Convert.ToString(dr_1["QuestionText"]),
                        SubjectType = Convert.ToString(dr_1["SubjectType"]),
                        PaperYear = Convert.ToString(dr_1["PaperYear"]),
                        ImageFilePath = Convert.ToString(dr_1["ImageFilePath"]),
                        VideoFilePath = Convert.ToString(dr_1["VideoFilePath"]),
                        PdfDocFilePath = Convert.ToString(dr_1["PdfDocFilePath"])




                    });
                    ViewBag.SubjectID = SubjectID;
                }
            }

            return lstRet;
        }
        public List<object> getChapterContentsQuestionBySubjectIDAndVariat(int SubjectID,string variant)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetChaptersContents_BySubjectIdAndVariant", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID),new KeyValuePair<string, object>("Variant",variant) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {


                        RowNumber = Convert.ToInt32(dr_1["RowNumber"]),
                        ContentsHolder = Convert.ToInt32(dr_1["ContentsHolder"]),
                        SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        ImageFileName = Convert.ToString(dr_1["ImageFileName"]),
                        VideoFileName = Convert.ToString(dr_1["VideoFileName"]),
                        // OriginalFileName = Convert.ToString(dr_1["OriginalFileName"]),
                        URL = Convert.ToString(dr_1["URL"]),
                        QuestionText = Convert.ToString(dr_1["QuestionText"]),
                        SubjectType = Convert.ToString(dr_1["SubjectType"]),
                        PaperYear = Convert.ToString(dr_1["PaperYear"]),
                        ImageFilePath = Convert.ToString(dr_1["ImageFilePath"]),
                        VideoFilePath = Convert.ToString(dr_1["VideoFilePath"]),
                        PdfDocFilePath = Convert.ToString(dr_1["PdfDocFilePath"])




                    });
                    ViewBag.SubjectID = SubjectID;
                }
            }

            return lstRet;
        }
        public List<object> getTopicalContentsBySubTopicID(int SubjectID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetChaptersContents_BySubjectId", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectID", SubjectID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {


                        RowNumber = Convert.ToInt32(dr_1["RowNumber"]),
                        ContentsHolder = Convert.ToInt32(dr_1["ContentsHolder"]),
                        SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        ImageFileName = Convert.ToString(dr_1["ImageFileName"]),
                        VideoFileName = Convert.ToString(dr_1["VideoFileName"]),
                        // OriginalFileName = Convert.ToString(dr_1["OriginalFileName"]),
                        URL = Convert.ToString(dr_1["URL"]),
                        // QuestionText = Convert.ToString(dr_1["QuestionText"]),
                        SubjectType = Convert.ToString(dr_1["SubjectType"]),
                        PaperYear = Convert.ToString(dr_1["PaperYear"]),
                        ImageFilePath = Convert.ToString(dr_1["ImageFilePath"]),
                        VideoFilePath = Convert.ToString(dr_1["VideoFilePath"]),
                        PdfDocFilePath = Convert.ToString(dr_1["PdfDocFilePath"])




                    });
                    ViewBag.SubjectID = SubjectID;
                }
            }

            return lstRet;
        }
        public List<ShowVideoViewModel> getShowVideoViewModelByID(int SubjectsChaptersID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<ShowVideoViewModel> lstRet = new List<ShowVideoViewModel>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetChaptersContents_1", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("SubjectsChaptersID", SubjectsChaptersID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new ShowVideoViewModel
                    {
                        
                        RowNumber = Convert.ToInt32(dr_1["RowNumber"]),
                        ContentsHolder = Convert.ToInt32(dr_1["ContentsHolder"]),
                        SubjectsChaptersID = Convert.ToInt32(dr_1["SubjectsChaptersID"]),
                        ChapterCode = Convert.ToString(dr_1["ChapterCode"]),
                        ChapterName = Convert.ToString(dr_1["ChapterName"]),
                        SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        ImageFileName = Convert.ToString(dr_1["ImageFileName"]),
                        VideoFileName = Convert.ToString(dr_1["VideoFileName"]),
                        URL = Convert.ToString(dr_1["URL"]),
                        QuestionText = Convert.ToString(dr_1["QuestionText"]),

                        ImageFilePath = Convert.ToString(dr_1["ImageFilePath"]),
                        PdfDocFile = Convert.ToString(dr_1["PdfDocFile"]),
                        VideoFilePath = Convert.ToString(dr_1["VideoFilePath"]),
                        PdfDocFilePath = Convert.ToString(dr_1["PdfDocFilePath"]),
                       
                    });
                    ViewBag.SubjectsChaptersID = SubjectsChaptersID;
                }
            }

            return lstRet;
        }



    }
}
