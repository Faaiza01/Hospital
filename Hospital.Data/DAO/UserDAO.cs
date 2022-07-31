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
                var prescription = context.Prescription.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (prescription != null)
                    continue;
                dAppointmentHistoryDto.AppointmentId = item.AppointmentId;
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

        public void Prescribe(HospitalContext context, PrescribeDto prescribeDto, int id)
        {
            Prescription prescription = new Prescription();
            prescription.AppointmentId = id;
            prescription.Symptoms = prescribeDto.Symptoms;
            prescription.Diseases = prescribeDto.Diseases;
            prescription.Allergies = prescribeDto.Allergies;
            prescription.Prescriptions = prescribeDto.Prescription;
            context.Prescription.Add(prescription);
            context.SaveChanges();
        }

        public List<DPrescriptionListDto> GetPrescriptionList(HospitalContext context, int userId)
        {
            List<DPrescriptionListDto> dPrescriptionListDtos = new List<DPrescriptionListDto>();
            var myAppointment = context.Appointment.Where(x => x.DoctorId == userId).ToList();
            foreach (var item in myAppointment)
            {
                DPrescriptionListDto dPrescriptionListDto = new DPrescriptionListDto();
                var myPatient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
                var prescriptions = context.Prescription.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (prescriptions == null)
                    continue;
                dPrescriptionListDto.PatientName = myPatient?.FirstName + ' ' + myPatient?.LastName;
                dPrescriptionListDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                dPrescriptionListDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                dPrescriptionListDto.Symptoms = prescriptions.Symptoms;
                dPrescriptionListDto.Diseases = prescriptions.Diseases;
                dPrescriptionListDto.Allergies = prescriptions.Allergies;
                dPrescriptionListDto.Prescriptions = prescriptions.Prescriptions;
                dPrescriptionListDtos.Add(dPrescriptionListDto);
            }
            return dPrescriptionListDtos;
        }

        public List<PPrescriptionListDto> GetPatientPrescriptionList(HospitalContext context, int userId)
        {
            List<PPrescriptionListDto> pPrescriptionListDtos = new List<PPrescriptionListDto>();
            var myAppointment = context.Appointment.Where(x => x.PatientId == userId).ToList();
            foreach (var item in myAppointment)
            {
                PPrescriptionListDto pPrescriptionListDto = new PPrescriptionListDto();
                var myDoctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var prescriptions = context.Prescription.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (prescriptions == null)
                    continue;
                pPrescriptionListDto.DoctorsName = myDoctor?.FirstName + ' ' + myDoctor?.LastName;
                pPrescriptionListDto.Fee = myDoctor.ConsultancyFee;
                pPrescriptionListDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                pPrescriptionListDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                pPrescriptionListDto.Symptoms = prescriptions?.Symptoms;
                pPrescriptionListDto.Diseases = prescriptions?.Diseases;
                pPrescriptionListDto.Allergies = prescriptions?.Allergies;
                pPrescriptionListDto.Prescriptions = prescriptions?.Prescriptions;
                pPrescriptionListDtos.Add(pPrescriptionListDto);
            }
            return pPrescriptionListDtos;
        }

        public List<AllPrescriptionListDto> GetAllPrescriptionList(HospitalContext context)
        {
            List<AllPrescriptionListDto> allPrescriptionListDtos = new List<AllPrescriptionListDto>();
            var appointments = context.Appointment.ToList();
            foreach (var item in appointments)
            {
                AllPrescriptionListDto prescriptionListDto = new AllPrescriptionListDto();
                var patient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
                var doctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var prescriptions = context.Prescription.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
                if (prescriptions == null)
                    continue;
                prescriptionListDto.PatientName = patient?.FirstName + ' ' + patient?.LastName;
                prescriptionListDto.DoctorName = doctor?.FirstName + ' ' + doctor?.LastName;
                prescriptionListDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                prescriptionListDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                prescriptionListDto.Symptoms = prescriptions?.Symptoms;
                prescriptionListDto.Diseases = prescriptions?.Diseases;
                prescriptionListDto.Allergies = prescriptions?.Allergies;
                prescriptionListDto.Prescriptions = prescriptions?.Prescriptions;
                allPrescriptionListDtos.Add(prescriptionListDto);
            }
            return allPrescriptionListDtos;
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
