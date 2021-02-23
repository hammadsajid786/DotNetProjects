using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSlider.Models
{
    public class Variants
    {
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> VariantList
        {
            get;
            set;
        }
    }
}