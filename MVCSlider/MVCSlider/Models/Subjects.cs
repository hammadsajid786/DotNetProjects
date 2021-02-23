using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class Subjects
    {

        public int SubjectID { get; set; }
        public int SubjectType { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectTypeDesc { get; set; }
        public string SubjectYear { get; set; }
        public int ParentSubjectID { get; set; }
        public byte[] SubjectImage { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }


    }
}