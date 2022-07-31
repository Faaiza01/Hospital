using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class AppointmentDetailsDto
    {
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string DoctorName { get; set; }
        public decimal? ConsultancyFee { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Status { get; set; }
    }
}
