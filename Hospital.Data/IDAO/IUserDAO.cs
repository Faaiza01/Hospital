using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.Models.Domain;
using Hospital.Data.Repository;


namespace Hospital.Data.IDAO
{
    public interface IUserDAO
    {
        IList<Users> GetUsers(HospitalContext context);
        IList<Users> GetRegistered(HospitalContext context);
        IList<Users> GetListOfDoctors(HospitalContext context);
        Users GetDoctorsData(HospitalContext context, int userId);
        void EditDoctor(HospitalContext context, Users doctor, int userId);
        void DeleteDoctor(HospitalContext context, int id);
        List<DAppointmentHistory> GetDoctorAppointmentHistory(HospitalContext context, int userId);
        void CancelAppointmentByDoctor(HospitalContext context, int appointmentId);
        void Treatment(HospitalContext context, TreatmentDto prescribeDto, int id);
        List<DTreatmentListDto> GetTreatmentList(HospitalContext context, int userId);
        List<PTreatmentListDto> GetPatientTreatmentList(HospitalContext context, int userId);
        List<AllTreatmentListDto> GetAllTreatmentList(HospitalContext context);
        void AddUser(HospitalContext context, Users Users);
        void RemovePatient(HospitalContext context, string identityId);
        Users GetUserData(HospitalContext context, string id);
        Users GetLoggedInUserData(HospitalContext context, string IdentityId);

    }
}
