using Hospital.Data.Models.Domain;
using Hospital.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Hospital.Data.IDAO
{
    public interface IHospitalDAO
    {
        List<Users> GetDoctorByDepartment(HospitalContext context);
        List<Users> GetDoctorByDepartment(HospitalContext context, string department);
        decimal GetDoctorsFee(HospitalContext context, string userId);
        void BookAppointment(HospitalContext context, Appointment appointment);
        List<PAppointmentHistoryDto> GetPatientAppointmentHistory(HospitalContext context, int userId);
        void CancelAppointment(HospitalContext context, int appointmentId);
        List<AppointmentDetailsDto> AppointmentDetail(HospitalContext context);

    }
}
