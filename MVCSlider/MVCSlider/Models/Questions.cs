using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class Questions
    {
        public int QuestionID { get; set; }
        public  string URL { get; set; }
        public int SubjectsChaptersID { get; set; }
        public string QuestionText { get; set; }
        public string ImageFileName { get; set; }
        public string VideoFileName { get; set; }
        public string PaperYear { get; set; }
        public string ImageFilePath { get; set; }
        public string VideoFilePath { get; set; }
        public string PdfDocFile { get; set; }
        public string PdfDocFilePath { get; set; }
        public bool isfree { get; set; }

        public int subID { get; set; }
    }
}