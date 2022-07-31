using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class PAppointmentHistoryDto
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public decimal? ConsultancyFee { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Status { get; set; }
        public bool IsCancelledByPatient { get; set; }
        public bool IsCancelledByDoctor { get; set; }
    }
}
