using Job.Data;
using Job.Data.DAO;
using Job.Data.IDAO;
using Job.Data.Models.Domain;
using Job.Data.Repository;
using Job.Services.IService;
using Job.Services.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Job.Services.Service
{
    public class JobService : IJobService
    {
        private IJobDAO JobDAO;
  
        public JobService()
        {
            JobDAO = new JobDAO();
        }

        public List<Users> GetDoctorByDepartment()
        {
            using (var context = new JobContext())
            {
               return JobDAO.GetDoctorByDepartment(context);
            }
        }

        public List<Users> GetDoctorByDepartment(string department)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetDoctorByDepartment(context, department);
            }
        }

        public decimal GetDoctorsFee(string userId)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetDoctorsFee(context, userId);
            }
        }

        public void BookAppointment(BookAppointmentDto bookAppointmentDto, int userId)
        {
            Appointment appointment = new Appointment()
            {
                PatientId = userId,
                DoctorId = Int32.Parse(bookAppointmentDto.Doctor),
                AppointmentDateTime = bookAppointmentDto.AppointmentDateTime,
                Status = true
            };

            using (var context = new JobContext())
            {
                JobDAO.BookAppointment(context, appointment);//Add job
                context.SaveChanges();
            }
        }

        public IList<PAppointmentHistoryDto> GetPatientAppointmentHistory(int userId)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetPatientAppointmentHistory(context, userId);
            }
        }

        public void CancelAppointment(int appointmentId)
        {
            using (var context = new JobContext())
            {
                JobDAO.CancelAppointment(context, appointmentId);
            }
        }

        public IList<AppointmentDetailsDto> AppointmentDetail()
        {
            using (var context = new JobContext())
            {
                return JobDAO.AppointmentDetail(context);
            }
        }
   
    }
}
