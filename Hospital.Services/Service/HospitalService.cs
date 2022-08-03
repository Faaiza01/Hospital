using Hospital.Data;
using Hospital.Data.DAO;
using Hospital.Data.IDAO;
using Hospital.Data.Models.Domain;
using Hospital.Data.Repository;
using Hospital.Services.IService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hospital.Services.Service
{
    public class HospitalService : IHospitalService
    {
        private IHospitalDAO HospitalDAO;
  
        public HospitalService()
        {
            HospitalDAO = new HospitalDAO();
        }

        public List<Users> GetDoctorByDepartment()
        {
            using (var context = new HospitalContext())
            {
               return HospitalDAO.GetDoctorByDepartment(context);
            }
        }

        public List<Users> GetDoctorByDepartment(string department)
        {
            using (var context = new HospitalContext())
            {
                return HospitalDAO.GetDoctorByDepartment(context, department);
            }
        }

        public decimal GetDoctorsFee(string userId)
        {
            using (var context = new HospitalContext())
            {
                return HospitalDAO.GetDoctorsFee(context, userId);
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

            using (var context = new HospitalContext())
            {
                HospitalDAO.BookAppointment(context, appointment);//Add Hospital
                context.SaveChanges();
            }
        }

        public IList<PAppointmentHistoryDto> GetPatientAppointmentHistory(int userId)
        {
            using (var context = new HospitalContext())
            {
                return HospitalDAO.GetPatientAppointmentHistory(context, userId);
            }
        }

        public void CancelAppointment(int appointmentId)
        {
            using (var context = new HospitalContext())
            {
                HospitalDAO.CancelAppointment(context, appointmentId);
            }
        }

        public IList<AppointmentDetailsDto> AppointmentDetail()
        {
            using (var context = new HospitalContext())
            {
                return HospitalDAO.AppointmentDetail(context);
            }
        }
   
    }
}
