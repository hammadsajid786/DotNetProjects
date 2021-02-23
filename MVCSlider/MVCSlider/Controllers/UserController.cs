using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;

namespace MVCSlider.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["LoginID"] == null || Convert.ToString(Session["LoginID"]) == "")
            {
                Response.Redirect("~/Home/Index");
            }

            dynamic lstRet = new StudentsController().getStudentSelectedSubjectsByID(Convert.ToInt32(Session["StudentID"]));

            return View(lstRet);
        }


        // GET: Chapters
        public ActionResult Chapters(int Sub)
        {
            if (Session["LoginID"] == null || Convert.ToString(Session["LoginID"]) == "")
            {
                Response.Redirect("~/Home/Index");
            }

            List<SubjectChapters> lstRet = new SubjectsController().getChaptersExerciseBySubjectsID(Sub);

            return View(lstRet);
        }


        public ActionResult ChaptersDetails(int SubChapID)
        {
            if (Session["LoginID"] == null || Convert.ToString(Session["LoginID"]) == "")
            {
                Response.Redirect("~/Home/Index");
            }

            List<object> lstRet = new List<object>();
            lstRet = new SubjectsController().getChapterContentsByID(SubChapID);
            return View(lstRet);
        }

    }
}