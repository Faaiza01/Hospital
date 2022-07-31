using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class DAppointmentHistory
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Status { get; set; }
        public bool IsCancelledByPatient { get; set; }
        public bool IsCancelledByDoctor { get; set; }
    }
}
