using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Data
{
    public class PrescribeDto
    {
        public int AppointmentId { get; set; }
        public string Diseases { get; set; }
        public string Symptoms { get; set; }
        public string Allergies { get; set; }
        public string Prescription { get; set; }
    }
}
