using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;
using MVCSlider.App_Code;

namespace MVCSlider.Controllers
{
    public class StudentsController : Controller
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

            List<Students> modelStudentsList = getStudents();
            return View(modelStudentsList);
        }

        public ActionResult CRUD(int ID)
        {
            verifySessionforAdmin();

            List<object> lstModels = new List<object>();
            Students modelStudent = getStudentByID(ID);
            lstModels.Add(modelStudent);
            List<StudentsSubjects> modelStudentsSubjects = getStudentSubjectsByID(ID);
            lstModels.Add(modelStudentsSubjects);
            ViewBag.Error = "";
            return View(lstModels);
        }


        [HttpPost]
        public ActionResult CRUD(FormCollection collection)
        {
            List<object> lstModels = new List<object>();
            List<string> lstSelectedSubjects = new List<string>();
            string vMode = collection["Mode"];

            Students modelStudent = new Students()
            {
                StudentNumber = Convert.ToString(collection["StudentNumber"]),
                FirstName = Convert.ToString(collection["FirstName"]),
                MiddleInit = Convert.ToString(collection["MiddleInit"]),
                LastName = Convert.ToString(collection["LastName"]),
                DateofBirth = Convert.ToString(collection["DateofBirth"]) == "" ? DateTime.MinValue : Convert.ToDateTime(collection["DateofBirth"]),
                BatchCode = Convert.ToString(collection["BatchCode"]),
                Address1 = Convert.ToString(collection["Address1"]),
                Address2 = Convert.ToString(collection["Address2"]),
                City = Convert.ToString(collection["City"]),
                Zipcode = Convert.ToString(collection["Zipcode"]),
                StateCode = Convert.ToString(collection["StateCode"]),
                UserID = Convert.ToInt32(collection["UserID"]),
                Password = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(collection["Password"]))
            };

            if (string.IsNullOrEmpty(collection["StudentID"]))
            {
                modelStudent.StudentID = 0;
            }
            else
            {
                modelStudent.StudentID = Convert.ToInt32(collection["StudentID"]);
            }

            if (!string.IsNullOrEmpty(collection["SelectedSubjects"]))
            {
                lstSelectedSubjects = Convert.ToString(collection["SelectedSubjects"]).Split(',').ToList();
            }

            MSSQL_DAC clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("StudentID",modelStudent.StudentID),
                    new KeyValuePair<string,object>("StudentNumber",modelStudent.StudentNumber),
                    new KeyValuePair<string,object>("FirstName",modelStudent.FirstName),
                    new KeyValuePair<string,object>("MiddleInit",modelStudent.MiddleInit),
                    new KeyValuePair<string,object>("LastName",modelStudent.LastName),
                    new KeyValuePair<string,object>("DateofBirth",modelStudent.DateofBirth),
                    new KeyValuePair<string,object>("BatchCode",modelStudent.BatchCode),
                    new KeyValuePair<string,object>("Address1",modelStudent.Address1),
                    new KeyValuePair<string,object>("Address2",modelStudent.Address2),
                    new KeyValuePair<string,object>("City",modelStudent.City),
                    new KeyValuePair<string,object>("Zipcode",modelStudent.Zipcode),
                    new KeyValuePair<string,object>("StateCode",modelStudent.StateCode),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"]),
                    new KeyValuePair<string,object>("Mode",vMode),
                    new KeyValuePair<string,object>("Password",modelStudent.Password),
                    new KeyValuePair<string,object>("SelectedSubjects",String.Join(",",lstSelectedSubjects.ToArray()))
                    };

            clsDAC.Execute_Procedure("sp_StudentsCRUD", lstData);
            ViewBag.Error = clsDAC.Error_Message;

            if (ViewBag.Error.Trim() == "")
            {
                Response.Redirect("~/Students/List");
            }

            modelStudent.Password = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(modelStudent.Password));
            lstModels.Add(modelStudent);
            List<StudentsSubjects> modelStudentsSubjects = getStudentSubjectsByID(0);
            lstModels.Add(modelStudentsSubjects);

            return View(lstModels);
        }



        //---Getting Students
        public List<Students> getStudents()
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetStudents");
            List<Students> lstStudents = new List<Students>();
            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstStudents.Add(new Students()
                    {
                        StudentID = Convert.ToInt32(dr_1["StudentID"]),
                        StudentNumber = Convert.ToString(dr_1["StudentNumber"]),
                        FirstName = Convert.ToString(dr_1["FirstName"]),
                        MiddleInit = Convert.ToString(dr_1["MiddleInit"]),
                        LastName = Convert.ToString(dr_1["LastName"]),
                        DateofBirth = dr_1["DateofBirth"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr_1["DateofBirth"]),
                        BatchCode = Convert.ToString(dr_1["BatchCode"]),
                        Address1 = Convert.ToString(dr_1["Address1"]),
                        Address2 = Convert.ToString(dr_1["Address2"]),
                        City = Convert.ToString(dr_1["City"]),
                        Zipcode = Convert.ToString(dr_1["Zipcode"]),
                        StateCode = Convert.ToString(dr_1["StateCode"]),
                        UserID = Convert.ToInt32(dr_1["UserID"])
                    });
                }
            }

            return lstStudents;
        }

        //---Getting Student by ID
        public Students getStudentByID(int StudentID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            Students clsStudentRet = new Students();

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetStudents", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("StudentID", StudentID) });

            if (dt_1 != null && dt_1.Rows.Count > 0)
            {
                DataRow dr_1 = dt_1.Rows[0];

                clsStudentRet = new Students()
                {
                    StudentID = Convert.ToInt32(dr_1["StudentID"]),
                    StudentNumber = Convert.ToString(dr_1["StudentNumber"]),
                    FirstName = Convert.ToString(dr_1["FirstName"]),
                    MiddleInit = Convert.ToString(dr_1["MiddleInit"]),
                    LastName = Convert.ToString(dr_1["LastName"]),
                    DateofBirth = dr_1["DateofBirth"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr_1["DateofBirth"]),
                    BatchCode = Convert.ToString(dr_1["BatchCode"]),
                    Address1 = Convert.ToString(dr_1["Address1"]),
                    Address2 = Convert.ToString(dr_1["Address2"]),
                    City = Convert.ToString(dr_1["City"]),
                    Zipcode = Convert.ToString(dr_1["Zipcode"]),
                    StateCode = Convert.ToString(dr_1["StateCode"]),
                    UserID = Convert.ToInt32(dr_1["UserID"]),
                    Password = Convert.ToString(dr_1["Password"]) == "" ? "" : System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Convert.ToString(dr_1["Password"])))
                };
            }
            return clsStudentRet;
        }

        //---
        public List<StudentsSubjects> getStudentSubjectsByID(int StudentID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<StudentsSubjects> clsStudentSubject = new List<StudentsSubjects>();

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetStudentsSubjects", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("StudentID", StudentID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    clsStudentSubject.Add(new StudentsSubjects()
                                            {
                                                SubjectID = Convert.ToInt32(dr_1["SubjectID"]),
                                                SubjectName = Convert.ToString(dr_1["SubjectName"]),
                                                SubjectCode = Convert.ToString(dr_1["SubjectCode"]),
                                                IsSelected = Convert.ToBoolean(dr_1["IsSelected"])
                                            }
                                         );
                }
            }

            return clsStudentSubject;
        }


        public dynamic getStudentSelectedSubjectsByID(int StudentID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            List<object> lstRet = new List<object>();


            dt_1 = clsDAC.Read_DT_Procedure("sp_GetStudentsSelectedSubjects", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("StudentID", StudentID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                foreach (DataRow dr_1 in dt_1.Rows)
                {
                    lstRet.Add(new
                    {
                        StudentNumber = Convert.ToString(dr_1["StudentNumber"]),
                        FirstName = Convert.ToString(dr_1["FirstName"]),
                        LastName = Convert.ToString(dr_1["LastName"]),
                        BatchCode = Convert.ToString(dr_1["BatchCode"]),
                        SubjectID = Convert.ToString(dr_1["SubjectID"]),
                        SubjectCode = Convert.ToString(dr_1["SubjectCode"]),
                        SubjectName = Convert.ToString(dr_1["SubjectName"]),
                        SubjectImage = (byte[])(dr_1["SubjectImage"])
                    });
                }
            }

            return lstRet;
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            MSSQL_DAC clsDAC = null;
            clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("StudentID",id),
                    new KeyValuePair<string,object>("CreatedBy",Session["UserID"]),
                    new KeyValuePair<string,object>("Mode","Delete")
                    };

            clsDAC.Execute_Procedure("sp_StudentsCRUD", lstData);

            ViewBag.Error = clsDAC.Error_Message;

            if (clsDAC.Error_Message.Trim() == "")
            {
                Response.Redirect("~/Students/List?removed=1");
            }
            else
            {
                Response.Redirect("~/Students/List?removed=0");
            }

            return View();
        }


    }
}