using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Data.Models.Domain
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string AppointmentDateTime { get; set; }
    }
}
