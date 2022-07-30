using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Job.Data
{
    public class DoctorsModel
    {
        public IEnumerable<SelectListItem> DoctorsData
        {
            get;
            set;
        }
    }
}
