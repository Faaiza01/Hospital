using Hospital.Data;
using Hospital.Data.DAO;
using Hospital.Data.IDAO;
using Hospital.Data.Models.Domain;
using Hospital.Data.Repository;
using Hospital.Services.IService;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services.Service
{
    public class UserService : IUserService
    {
        private IUserDAO UserDAO;

        public UserService()
        {
            UserDAO = new UserDAO();
        }

        public IList<Users> GetUsers()
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetUsers(context);
            }
        }
        public IList<Users> GetRegistered()
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetRegistered(context);
            }
        }

        public IList<Users> GetListOfDoctors()
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetListOfDoctors(context);
            }
        }

        public Users GetDoctorData(int userId)
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetDoctorsData(context,userId);
            }
        }

        public void EditDoctor(Users users, int userId)
        {
            using (var context = new HospitalContext())
            {
                Users doctor = new Users();
                doctor.FirstName = users.FirstName;
                doctor.LastName = users.LastName;
                doctor.ContactNumber = users.ContactNumber;
                doctor.Gender = users.Gender;
                doctor.Specialization = users.Specialization;
                doctor.ConsultancyFee = users.ConsultancyFee;

                UserDAO.EditDoctor(context, doctor, userId);//Update existing Hospital         
                context.SaveChanges();
            }
        }

        public void DeleteDoctor(int id)
        {
            using (var context = new HospitalContext())
            {
                UserDAO.DeleteDoctor(context, id);
            }
        }


        public IList<DAppointmentHistory> GetDoctorAppointmentHistory(int userId)
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetDoctorAppointmentHistory(context, userId);
            }
        }
        public void CancelAppointmentByDoctor(int appointmentId)
        {
            using (var context = new HospitalContext())
            {
                UserDAO.CancelAppointmentByDoctor(context, appointmentId);
            }
        }

        public void Treatment( TreatmentDto prescribeDto, int id)
        {

            using (var context = new HospitalContext())
            {
                UserDAO.Treatment(context, prescribeDto, id);//Add user
                context.SaveChanges();
            }
        }

        public IList<DTreatmentListDto> GetTreatmentList(int userId)
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetTreatmentList(context, userId);
            }
        }

        public IList<PTreatmentListDto> GetPatientTreatmentList(int userId)
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetPatientTreatmentList(context, userId);
            }
        }

        public IList<AllTreatmentListDto> GetAllTreatmentList()
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetAllTreatmentList(context);
            }
        }

        public void AddUser(Users Users)
        {

            using (var context = new HospitalContext())
            {
                UserDAO.AddUser(context, Users);//Add user
                context.SaveChanges();
            }
        }

        public void RemovePatient(string id)
        {
            using (var context = new HospitalContext())
            {
                UserDAO.RemovePatient(context, id);
            }
        }

        public Users GetUserData(string id)
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetUserData(context, id);
            }
        }


        public Users GetLoggedInUserData(string IdentityId)
        {
            using (var context = new HospitalContext())
            {
                return UserDAO.GetLoggedInUserData(context, IdentityId);
            }
        }


    }
}
