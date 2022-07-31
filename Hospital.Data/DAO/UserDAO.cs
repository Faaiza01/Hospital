using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Data.IDAO;
using Job.Data.Models.Domain;
using Job.Data.Repository;
using System.Data.Entity;


namespace Job.Data.DAO
{
    public class UserDAO : IUserDAO
    {
        public IList<Users> GetUsers(JobContext context)
        {
            return context.Users.ToList();
        }

        public IList<Users> GetListOfDoctors(JobContext context)
        {
            return context.Users.Where(x => x.Role == "Doctor").ToList();
        }

        public Users GetDoctorsData(JobContext context, int userId)
        {
            return context.Users.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public void EditDoctor(JobContext context, Users doctor, int userId)
        {
            context.Users.Find(userId).FirstName = doctor.FirstName;
            context.Users.Find(userId).LastName = doctor.LastName;
            context.Users.Find(userId).Gender = doctor.Gender;
            context.Users.Find(userId).ContactNumber = doctor.ContactNumber;
            context.Users.Find(userId).Specialization = doctor.Specialization;
            context.Users.Find(userId).ConsultancyFee = doctor.ConsultancyFee;
            context.SaveChanges();
        }

        public void DeleteDoctor(JobContext context, int id)
        {
            var doctor = context.Users.Where(x => x.UserId == id).ToList();          
            context.Users.Remove(context.Users.Find(id));
            context.SaveChanges();
        }
        public List<DAppointmentHistory> GetDoctorAppointmentHistory(JobContext context, int userId)
        {
            List<DAppointmentHistory> appointmentHistoryDtos = new List<DAppointmentHistory>();
            var myAppointments = context.Appointment.Where(x => x.DoctorId == userId).ToList();
            foreach (var item in myAppointments)
            {
                DAppointmentHistory dAppointmentHistoryDto = new DAppointmentHistory();

                var myPatient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
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

        public void CancelAppointmentByDoctor(JobContext context, int appointmentId)
        {
            context.Appointment.Find(appointmentId).IsCancelledByDoctor = true;
            context.Appointment.Find(appointmentId).Status = false;
            context.SaveChanges();
        }

        public void Prescribe(JobContext context, PrescribeDto prescribeDto, int id)
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

        public List<DPrescriptionListDto> GetPrescriptionList(JobContext context, int userId)
        {
            List<DPrescriptionListDto> dPrescriptionListDtos = new List<DPrescriptionListDto>();
            var myAppointment = context.Appointment.Where(x => x.DoctorId == userId).ToList();
            foreach (var item in myAppointment)
            {
                DPrescriptionListDto dPrescriptionListDto = new DPrescriptionListDto();
                var myPatient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();
                var prescriptions = context.Prescription.Where(c => c.AppointmentId == item.AppointmentId).FirstOrDefault();
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


        public void AddUser(JobContext context, Users Users)
        {
            context.Users.Add(Users);
            context.SaveChanges();
        }

        public void RemoveUser(JobContext context, string identityId)
        {
            context.Users.Remove(context.Users.Where(c => c.IdentityId == identityId).FirstOrDefault());
            context.SaveChanges();
        }

        public Users GetUserData(JobContext context, string id)
        {
            return context.Users.ToList().Find(b => b.IdentityId == id);
        }

        public Users GetLoggedInUserData(JobContext context, string IdentityId)
        {
            return context.Users.Where(d => d.IdentityId == IdentityId).FirstOrDefault();
        }


        public void EditProfile(JobContext context, Users employer, string userId)
        {
            var Id = context.Users.Where(x => x.IdentityId == userId).Select(d => d.UserId).FirstOrDefault();
            context.Users.Find(Id).FirstName = employer.FirstName;
            context.Users.Find(Id).LastName = employer.LastName;
        }

        public string GetResumePath(JobContext context, string IdentityId)
        {
            return "";
        }


        public List<AppliedJobsList> GetUserAppliedJobs(JobContext context, string UserId)
        {
            List<AppliedJobsList> appliedJobsList = new List<AppliedJobsList>();
            var result = context.AppliedJobs.Where(x => x.UserIdentityId == UserId).ToList();
            foreach (var item in result)
            {
             
                var jobs = context.Employers.Where(x => x.JobId == item.JobId).FirstOrDefault();
                AppliedJobsList appliedJobsList1 = new AppliedJobsList();
                appliedJobsList1.JobId = item.JobId;
                appliedJobsList1.JobTitle = jobs?.JobTitle;
                appliedJobsList1.JobDescription = jobs?.JobDescription;
                appliedJobsList1.JobCategory = jobs?.JobCategory;
                appliedJobsList1.Salary = jobs?.Salary;
                appliedJobsList1.CompanyName = jobs?.CompanyName;
                appliedJobsList1.ComapanyEmail = jobs?.ComapanyEmail;
                appliedJobsList1.CompanyAddress = jobs?.CompanyAddress;
                appliedJobsList1.CompanyWebsite = jobs?.CompanyWebsite;
                appliedJobsList1.AppliedJobDate = item.AppliedJobDate;
                appliedJobsList1.AppliedJobId = item.AppliedJobId;
                appliedJobsList.Add(appliedJobsList1);
            }
            return appliedJobsList;
        }

        public List<SavedJobList> GetUserSavedJobs(JobContext context, string UserId)
        {
            List<SavedJobList> savedJobsList = new List<SavedJobList>();
            var result = context.SavedJobs.Where(x => x.UserIdentityId == UserId).ToList();
            foreach (var item in result)
            {

                var jobs = context.Employers.Where(x => x.JobId == item.JobId).FirstOrDefault();
                SavedJobList savedJobsList1 = new SavedJobList();
                savedJobsList1.JobId = item.JobId;
                savedJobsList1.JobTitle = jobs?.JobTitle;
                savedJobsList1.JobDescription = jobs?.JobDescription;
                savedJobsList1.JobCategory = jobs?.JobCategory;
                savedJobsList1.Salary = jobs?.Salary;
                savedJobsList1.CompanyName = jobs?.CompanyName;
                savedJobsList1.ComapanyEmail = jobs?.ComapanyEmail;
                savedJobsList1.CompanyAddress = jobs?.CompanyAddress;
                savedJobsList1.CompanyWebsite = jobs?.CompanyWebsite;
                savedJobsList1.SavedJobId = item.SavedJobId;
                savedJobsList.Add(savedJobsList1);
            }
            return savedJobsList;
        }


    }  
}
