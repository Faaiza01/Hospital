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
        void Prescribe(PrescribeDto prescribeDto, int id);
        IList<DPrescriptionListDto> GetPrescriptionList(int userId);
        IList<PPrescriptionListDto> GetPatientPrescriptionList(int userId);
        IList<AllPrescriptionListDto> GetAllPrescriptionList();
        void AddUser(Users Users);
        void RemovePatient(string id);
        Users GetUserData(string id);
        Users GetLoggedInUserData(string IdentityId);
    
    }
}
