using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class SubjectChapters
    {

        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int SubjectsChaptersID { get; set; }
        public string ChapterCode { get; set; }
        public string ChapterName { get; set; }
        public int ChapterType { get; set; }
        public int ParentChapterID { get; set; }

        public int SubjectType { get; set; }
        

    }
}