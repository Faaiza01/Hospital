using Job.Data;
using Job.Data.Models.Domain;
using Job.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.IService
{
    public interface IUserService
    {
        IList<Users> GetUsers();
        IList<Users> GetListOfDoctors();
        Users GetDoctorData(int userId);
        void EditDoctor(Users users, int userId);
        void DeleteDoctor(int id);
        IList<DAppointmentHistory> GetDoctorAppointmentHistory(int userId);
        void CancelAppointmentByDoctor(int appointmentId);
        void Prescribe(PrescribeDto prescribeDto, int id);
        void AddUser(Users Users);
        void RemoveUser(string id);
        Users GetUserData(string id);
        Users GetLoggedInUserData(string IdentityId);
        void EditProfile(EditProfileDto editProfileDto, string userId);
        string GetResumePath(string identityId);
        IList<AppliedJobsList> GetUserAppliedJobs(string UserId);
        IList<SavedJobList> GetUserSavedJobs(string UserId);
    }
}
