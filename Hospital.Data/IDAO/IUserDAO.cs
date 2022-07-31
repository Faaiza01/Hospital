using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Data.Models.Domain;
using Job.Data.Repository;


namespace Job.Data.IDAO
{
    public interface IUserDAO
    {
        IList<Users> GetRegistered(JobContext context);
        IList<Users> GetListOfDoctors(JobContext context);
        Users GetDoctorsData(JobContext context, int userId);
        void EditDoctor(JobContext context, Users doctor, int userId);
        void DeleteDoctor(JobContext context, int id);
        List<DAppointmentHistory> GetDoctorAppointmentHistory(JobContext context, int userId);
        void CancelAppointmentByDoctor(JobContext context, int appointmentId);
        void Prescribe(JobContext context, PrescribeDto prescribeDto, int id);
        List<DPrescriptionListDto> GetPrescriptionList(JobContext context, int userId);
        List<PPrescriptionListDto> GetPatientPrescriptionList(JobContext context, int userId);
        List<AllPrescriptionListDto> GetAllPrescriptionList(JobContext context);
        void AddUser(JobContext context, Users Users);
        void RemovePatient(JobContext context, string identityId);
        Users GetUserData(JobContext context, string id);
        Users GetLoggedInUserData(JobContext context, string IdentityId);
        void EditProfile(JobContext context, Users employer, string userId);
        string GetResumePath(JobContext context, string IdentityId);
        List<AppliedJobsList> GetUserAppliedJobs(JobContext context, string UserId);
        List<SavedJobList> GetUserSavedJobs(JobContext context, string UserId);
    }
}
