using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Data
{
    public class PPrescriptionListDto
    {
        public string DoctorsName { get; set; }
        public decimal Fee { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Symptoms { get; set; }
        public string Diseases { get; set; }
        public string Allergies { get; set; }
        public string Prescriptions { get; set; }
    }
}
