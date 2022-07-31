using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Models.Domain
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string Symptoms { get; set; }
        public string Diseases { get; set; }
        public string Allergies { get; set; }
        public string Prescriptions { get; set; }

    }
}
