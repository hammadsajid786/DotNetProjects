using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class ShowVideoViewModel
    {
        public int RowNumber { get; set; }
        public int ContentsHolder { get; set; }
        public int SubjectsChaptersID { get; set; }
        public string ChapterCode { get; set; }
        public string ChapterName { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string ImageFileName { get; set; }
        public string VideoFileName { get; set; }
        public string URL { get; set; }
        public string QuestionText { get; set; }
        public string ExerciseName { get; set; }
        public string ImageFilePath { get; set; }
        public string VideoFilePath { get; set; }
        public string PdfDocFile { get; set; }
        public string PdfDocFilePath { get; set; }

    }
}