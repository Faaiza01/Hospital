using Job.Data.Models.Domain;
using Job.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Job.Data.IDAO
{
    public interface IJobDAO
    {
        List<Users> GetDoctorByDepartment(JobContext context);
        List<Users> GetDoctorByDepartment(JobContext context, string department);
        decimal GetDoctorsFee(JobContext context, string userId);
        void BookAppointment(JobContext context, Appointment appointment);
        List<PAppointmentHistoryDto> GetPatientAppointmentHistory(JobContext context, int userId);
        void CancelAppointment(JobContext context, int appointmentId);
        List<AppointmentDetailsDto> AppointmentDetail(JobContext context);

    }
}
