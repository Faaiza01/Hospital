using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hospital.Data
{
    public class BookAppointmentDto
    {
        public IEnumerable<SelectListItem> DoctorsData
        {
            get;
            set;
        }
        public string Department { get; set; }
        public string Doctor { get; set; }
        public decimal ConsultancyFee { get; set; }
        public string AppointmentDateTime { get; set; }
    }
}
