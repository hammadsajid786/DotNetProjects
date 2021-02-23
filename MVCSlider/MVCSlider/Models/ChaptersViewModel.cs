using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.Models
{
    public class ChaptersViewModel
    {
       
        public IEnumerable<SelectListItem> Variants
        {
            get;
            set;
        }
        public List<SubjectChapters> SubjectChapters { get; set; }
        public List<Questions> Questions { get; set; }

        public List<Subjects> Subjects { get; set; }
    }
}