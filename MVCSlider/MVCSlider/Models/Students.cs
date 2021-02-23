using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSlider.Models
{
    public class Students
    {

        public int StudentID { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string BatchCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string StateCode { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public byte[] StudentImage { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }


    }

    public class StudentsSubjects
    {

        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public bool IsSelected { get; set; }
    }


}