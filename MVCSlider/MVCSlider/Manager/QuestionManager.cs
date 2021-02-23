using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;

namespace MVCSlider.Manager
{
    public class QuestionManager
    {
        public Questions GetQuestionByID(int QuestionID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            Questions clsQuestion = null;

            dt_1 = clsDAC.Read_DT_Procedure("GetQuestionByID", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("QuestionID", QuestionID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                DataRow dr_1 = dt_1.Rows[0];

                clsQuestion = new Questions()
                {
                    ImageFileName = Convert.ToString(dr_1["ImageFileName"]),
                    SubjectsChaptersID = (dr_1["SubjectsChaptersID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["SubjectsChaptersID"])),
                    QuestionID = (dr_1["QuestionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["QuestionID"])),
                    PaperYear = Convert.ToString(dr_1["PaperYear"]),
                    QuestionText = Convert.ToString(dr_1["QuestionText"]),
                    URL = Convert.ToString(dr_1["URL"]),
                    VideoFileName = Convert.ToString(dr_1["VideoFileName"]),
                    ImageFilePath = Convert.ToString(dr_1["ImageFilePath"]),
                    PdfDocFile = Convert.ToString(dr_1["PdfDocFile"]),
                    PdfDocFilePath = Convert.ToString(dr_1["PdfDocFilePath"]),
                    VideoFilePath = Convert.ToString(dr_1["VideoFilePath"]),


                };
            }

            return clsQuestion;


        }
    }
}