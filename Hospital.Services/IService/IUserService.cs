using Hospital.Data;
using Hospital.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.IService
{
    public interface IUserService
    {
        IList<Users> GetUsers();
        IList<Users> GetRegistered();
        IList<Users> GetListOfDoctors();
        Users GetDoctorData(int userId);
        void EditDoctor(Users users, int userId);
        void DeleteDoctor(int id);
        IList<DAppointmentHistory> GetDoctorAppointmentHistory(int userId);
        void CancelAppointmentByDoctor(int appointmentId);
        void Treatment(TreatmentDto prescribeDto, int id);
        IList<DTreatmentListDto> GetTreatmentList(int userId);
        IList<PTreatmentListDto> GetPatientTreatmentList(int userId);
        IList<AllTreatmentListDto> GetAllTreatmentList();
        void AddUser(Users Users);
        void RemovePatient(string id);
        Users GetUserData(string id);
        Users GetLoggedInUserData(string IdentityId);
    
    }
}
