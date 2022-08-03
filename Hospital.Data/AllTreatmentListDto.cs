using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class AllTreatmentListDto
    {
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string DoctorName { get; set; }
        public string Symptoms { get; set; }
        public string Diseases { get; set; }
        public string Allergies { get; set; }
        public string Medicines { get; set; }

    }
}
