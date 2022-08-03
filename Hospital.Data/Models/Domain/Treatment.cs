using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Models.Domain
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }
        public int AppointmentId { get; set; }
        public string Symptoms { get; set; }
        public string Diseases { get; set; }
        public string Allergies { get; set; }
        public string Medicines { get; set; }

    }
}
