using Hospital.Data.IDAO;
using Hospital.Data.Models.Domain;
using Hospital.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace Hospital.Data.DAO
{
    public class HospitalDAO : IHospitalDAO
    {
        public List<Users> GetDoctorByDepartment(HospitalContext context)
        {           
            //.Select(m => new SelectListItem { Text = m.FirstName, Value = m.UserId.ToString() })
            var result=   context.Users.Where(x => x.Role == "Doctor").ToList();
            return result;
        }

        public List<Users> GetDoctorByDepartment(HospitalContext context, string department)
        {
            //.Select(m => new SelectListItem { Text = m.FirstName, Value = m.UserId.ToString() })
            var result = context.Users.Where(x => x.Specialization== department).ToList();
            return result;
        }

        public decimal GetDoctorsFee(HospitalContext context, string userId)
        {
            int id = Int32.Parse(userId);
            return context.Users.Where(x => x.UserId == id).FirstOrDefault().ConsultancyFee;
          
        }

        public void BookAppointment(HospitalContext context, Appointment appointment)
        {
            context.Appointment.Add(appointment);
            context.SaveChanges();
        }

        public List<PAppointmentHistoryDto> GetPatientAppointmentHistory(HospitalContext context, int userId)
        {
            List<PAppointmentHistoryDto> appointmentHistoryDtos = new List<PAppointmentHistoryDto>();
            var myAppointments = context.Appointment.Where(x => x.PatientId == userId).ToList();
            foreach (var item in myAppointments)
            {
                PAppointmentHistoryDto pAppointmentHistoryDto = new PAppointmentHistoryDto();

                var myDoctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var prescription = context.Prescription.Where(x => x.AppointmentId == item.AppointmentId).FirstOrDefault();

                if (prescription != null)
                    continue;
                pAppointmentHistoryDto.AppointmentId = item.AppointmentId;
                pAppointmentHistoryDto.DoctorName = myDoctor?.FirstName + ' ' + myDoctor?.LastName;
                pAppointmentHistoryDto.ConsultancyFee = myDoctor?.ConsultancyFee;
                pAppointmentHistoryDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                pAppointmentHistoryDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                pAppointmentHistoryDto.Status = item?.Status ==  true ? "Active" : "Closed";
                pAppointmentHistoryDto.IsCancelledByPatient = item.IsCancelledByPatient;
                pAppointmentHistoryDto.IsCancelledByDoctor = item.IsCancelledByDoctor;
                appointmentHistoryDtos.Add(pAppointmentHistoryDto);
            }
            return appointmentHistoryDtos;
        }

        public void CancelAppointment(HospitalContext context, int appointmentId)
        {
            context.Appointment.Find(appointmentId).IsCancelledByPatient = true;
            context.Appointment.Find(appointmentId).Status = false;
            context.SaveChanges();
        }

        public List<AppointmentDetailsDto> AppointmentDetail(HospitalContext context)
        {
            List<AppointmentDetailsDto> appointmentDetailsDtos = new List<AppointmentDetailsDto>();
            var appointments = context.Appointment.ToList();
            foreach (var item in appointments)
            {
                AppointmentDetailsDto appointmentDetailsDto = new AppointmentDetailsDto();

                var doctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var patient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();

                appointmentDetailsDto.DoctorName = doctor?.FirstName + ' ' + doctor?.LastName;
                appointmentDetailsDto.ConsultancyFee = doctor?.ConsultancyFee;
                appointmentDetailsDto.PatientName = patient?.FirstName + ' ' + patient?.LastName;
                appointmentDetailsDto.Gender = patient?.Gender;
                appointmentDetailsDto.ContactNumber = patient?.ContactNumber;
                appointmentDetailsDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                appointmentDetailsDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                appointmentDetailsDto.Status = item?.Status == true ? "Active" : "Closed";
                appointmentDetailsDtos.Add(appointmentDetailsDto);
            }
            return appointmentDetailsDtos;
        }

  
    }

}
