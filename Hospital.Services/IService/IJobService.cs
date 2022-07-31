using Hospital.Data;
using Hospital.Data.Models.Domain;
using Hospital.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hospital.Services.IService
{
    public interface IHospitalService
    {
        List<Users> GetDoctorByDepartment();
        List<Users> GetDoctorByDepartment(string department);
        decimal GetDoctorsFee(string userId);
        void BookAppointment(BookAppointmentDto bookAppointmentDto, int userId);
        IList<PAppointmentHistoryDto> GetPatientAppointmentHistory(int userId);
        void CancelAppointment(int appointmentId);
        IList<AppointmentDetailsDto> AppointmentDetail();

    }
}
