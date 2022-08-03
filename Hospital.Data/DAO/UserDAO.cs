using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.IDAO;
using Hospital.Data.Models.Domain;
using Hospital.Data.Repository;
using System.Data.Entity;


namespace Hospital.Data.DAO
{
    public class UserDAO : IUserDAO
    {
        public IList<Users> GetUsers(HospitalContext context)
        {
            return context.Users.ToList();
        }
        public IList<Users> GetRegistered(HospitalContext context)
        {
            return context.Users.Where(x => x.Role == "Patient").ToList();
        }

        public IList<Users> GetListOfDoctors(HospitalContext context)
        {
            return context.Users.Where(x => x.Role == "Doctor").ToList();
        }

        public Users GetDoctorsData(HospitalContext context, int userId)
        {
            return context.Users.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public void EditDoctor(HospitalContext context, Users doctor, int userId)
        {
            context.Users.Find(userId).FirstName = doctor.FirstName;
            context.Users.Find(userId).LastName = doctor.LastName;
            context.Users.Find(userId).Gender = doctor.Gender;
            context.Users.Find(userId).ContactNumber = doctor.ContactNumber;
            context.Users.Find(userId).Specialization = doctor.Specialization;
            context.Users.Find(userId).ConsultancyFee = doctor.ConsultancyFee;
            context.SaveChanges();
        }

        public void DeleteDoctor(HospitalContext context, int id)
        {
            var doctor = context.Users.Where(x => x.UserId == id).ToList();          
            context.Users.Remove(context.Users.Find(id));
            context.SaveChanges();
        }
        public List<DAppointmentHistory> GetDoctorAppointmentHistory(HospitalContext context, int userId)
        {
            List<DAppointmentHistory> appointmentHistoryDtos = new List<DAppointmentHistory>();
            var myAppointments = context.Appointment.Where(x => x.DoctorId == userId).ToList();
            foreach (var item in myAppointments)
            {
                DAppointmentHistory dAppointmentHistoryDto = new DAppointmentHistory();

                var myPatient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
                var Treatment = context.Treatment.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (Treatment != null)
                    continue;
                dAppointmentHistoryDto.AppointmentId = item.AppointmentId;
                dAppointmentHistoryDto.PatientId = myPatient.PatientId;
                dAppointmentHistoryDto.PatientName = myPatient?.FirstName + ' ' + myPatient?.LastName;
                dAppointmentHistoryDto.Gender = myPatient?.Gender;
                dAppointmentHistoryDto.Email = myPatient?.Email;
                dAppointmentHistoryDto.ContactNumber = myPatient?.ContactNumber;
                dAppointmentHistoryDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                dAppointmentHistoryDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                dAppointmentHistoryDto.Status = item?.Status == true ? "Active" : "Closed";
                dAppointmentHistoryDto.IsCancelledByPatient = item.IsCancelledByPatient;
                dAppointmentHistoryDto.IsCancelledByDoctor = item.IsCancelledByDoctor;
                appointmentHistoryDtos.Add(dAppointmentHistoryDto);
            }
            return appointmentHistoryDtos;
        }

        public void CancelAppointmentByDoctor(HospitalContext context, int appointmentId)
        {
            context.Appointment.Find(appointmentId).IsCancelledByDoctor = true;
            context.Appointment.Find(appointmentId).Status = false;
            context.SaveChanges();
        }

        public void Treatment(HospitalContext context, TreatmentDto prescribeDto, int id)
        {
            Treatment Treatment = new Treatment();
            Treatment.AppointmentId = id;
            Treatment.Symptoms = prescribeDto.Symptoms;
            Treatment.Diseases = prescribeDto.Diseases;
            Treatment.Allergies = prescribeDto.Allergies;
            Treatment.Medicines = prescribeDto.Medicines;
            context.Treatment.Add(Treatment);
            context.SaveChanges();
        }

        public List<DTreatmentListDto> GetTreatmentList(HospitalContext context, int userId)
        {
            List<DTreatmentListDto> dTreatmentListDtos = new List<DTreatmentListDto>();
            var myAppointment = context.Appointment.Where(x => x.DoctorId == userId).ToList();
            foreach (var item in myAppointment)
            {
                DTreatmentListDto dTreatmentListDto = new DTreatmentListDto();
                var myPatient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
                var Treatments = context.Treatment.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (Treatments == null)
                    continue;
                dTreatmentListDto.PatientId = myPatient?.PatientId;
                dTreatmentListDto.PatientName = myPatient?.FirstName + ' ' + myPatient?.LastName;
                dTreatmentListDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                dTreatmentListDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                dTreatmentListDto.Symptoms = Treatments.Symptoms;
                dTreatmentListDto.Diseases = Treatments.Diseases;
                dTreatmentListDto.Allergies = Treatments.Allergies;
                dTreatmentListDto.Medicines = Treatments.Medicines;
                dTreatmentListDtos.Add(dTreatmentListDto);
            }
            return dTreatmentListDtos;
        }

        public List<PTreatmentListDto> GetPatientTreatmentList(HospitalContext context, int userId)
        {
            List<PTreatmentListDto> pTreatmentListDtos = new List<PTreatmentListDto>();
            var myAppointment = context.Appointment.Where(x => x.PatientId == userId).ToList();
            foreach (var item in myAppointment)
            {
                PTreatmentListDto pTreatmentListDto = new PTreatmentListDto();
                var myDoctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var Treatments = context.Treatment.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (Treatments == null)
                    continue;
                pTreatmentListDto.DoctorsName = myDoctor?.FirstName + ' ' + myDoctor?.LastName;
                pTreatmentListDto.Fee = myDoctor.ConsultancyFee;
                pTreatmentListDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                pTreatmentListDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                pTreatmentListDto.Symptoms = Treatments?.Symptoms;
                pTreatmentListDto.Diseases = Treatments?.Diseases;
                pTreatmentListDto.Allergies = Treatments?.Allergies;
                pTreatmentListDto.Medicines = Treatments?.Medicines;
                pTreatmentListDtos.Add(pTreatmentListDto);
            }
            return pTreatmentListDtos;
        }

        public List<AllTreatmentListDto> GetAllTreatmentList(HospitalContext context)
        {
            List<AllTreatmentListDto> allTreatmentListDtos = new List<AllTreatmentListDto>();
            var appointments = context.Appointment.ToList();
            foreach (var item in appointments)
            {
                AllTreatmentListDto TreatmentListDto = new AllTreatmentListDto();
                var patient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
                var doctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var Treatments = context.Treatment.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (Treatments == null)
                    continue;
                TreatmentListDto.PatientId = patient?.PatientId;
                TreatmentListDto.PatientName = patient?.FirstName + ' ' + patient?.LastName;
                TreatmentListDto.DoctorName = doctor?.FirstName + ' ' + doctor?.LastName;
                TreatmentListDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                TreatmentListDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                TreatmentListDto.Symptoms = Treatments?.Symptoms;
                TreatmentListDto.Diseases = Treatments?.Diseases;
                TreatmentListDto.Allergies = Treatments?.Allergies;
                TreatmentListDto.Medicines = Treatments?.Medicines;
                allTreatmentListDtos.Add(TreatmentListDto);
            }
            return allTreatmentListDtos;
        }
        public void AddUser(HospitalContext context, Users Users)
        {
            context.Users.Add(Users);
            context.SaveChanges();
        }

        public void RemovePatient(HospitalContext context, string identityId)
        {
            context.Users.Remove(context.Users.Where(c => c.IdentityId == identityId).FirstOrDefault());
            context.SaveChanges();
        }

        public Users GetUserData(HospitalContext context, string id)
        {
            return context.Users.ToList().Find(b => b.IdentityId == id);
        }

        public Users GetLoggedInUserData(HospitalContext context, string IdentityId)
        {
            return context.Users.Where(d => d.IdentityId == IdentityId).FirstOrDefault();
        }


    

 
    }  
}
