﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Data
{
    public class DPrescriptionListDto
    {
        public string PatientName { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Symptoms { get; set; }
        public string Diseases { get; set; }
        public string Allergies { get; set; }
        public string Prescriptions { get; set; }
    }
}