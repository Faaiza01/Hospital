using Job.Data;
using Job.Data.DAO;
using Job.Data.IDAO;
using Job.Data.Models.Domain;
using Job.Data.Repository;
using Job.Services.IService;
using Job.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Job.Services.Service
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
            using (var context = new JobContext())
            {
                return UserDAO.GetUsers(context);
            }
        }
        public IList<Users> GetRegistered()
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetRegistered(context);
            }
        }

        public IList<Users> GetListOfDoctors()
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetListOfDoctors(context);
            }
        }

        public Users GetDoctorData(int userId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetDoctorsData(context,userId);
            }
        }

        public void EditDoctor(Users users, int userId)
        {
            using (var context = new JobContext())
            {
                Users doctor = new Users();
                doctor.FirstName = users.FirstName;
                doctor.LastName = users.LastName;
                doctor.ContactNumber = users.ContactNumber;
                doctor.Gender = users.Gender;
                doctor.Specialization = users.Specialization;
                doctor.ConsultancyFee = users.ConsultancyFee;

                UserDAO.EditDoctor(context, doctor, userId);//Update existing job         
                context.SaveChanges();
            }
        }

        public void DeleteDoctor(int id)
        {
            using (var context = new JobContext())
            {
                UserDAO.DeleteDoctor(context, id);
            }
        }


        public IList<DAppointmentHistory> GetDoctorAppointmentHistory(int userId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetDoctorAppointmentHistory(context, userId);
            }
        }
        public void CancelAppointmentByDoctor(int appointmentId)
        {
            using (var context = new JobContext())
            {
                UserDAO.CancelAppointmentByDoctor(context, appointmentId);
            }
        }

        public void Prescribe( PrescribeDto prescribeDto, int id)
        {

            using (var context = new JobContext())
            {
                UserDAO.Prescribe(context, prescribeDto, id);//Add user
                context.SaveChanges();
            }
        }

        public IList<DPrescriptionListDto> GetPrescriptionList(int userId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetPrescriptionList(context, userId);
            }
        }

        public IList<PPrescriptionListDto> GetPatientPrescriptionList(int userId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetPatientPrescriptionList(context, userId);
            }
        }

        public IList<AllPrescriptionListDto> GetAllPrescriptionList()
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetAllPrescriptionList(context);
            }
        }

        public void AddUser(Users Users)
        {

            using (var context = new JobContext())
            {
                UserDAO.AddUser(context, Users);//Add user
                context.SaveChanges();
            }
        }

        public void RemovePatient(string id)
        {
            using (var context = new JobContext())
            {
                UserDAO.RemovePatient(context, id);
            }
        }

        public Users GetUserData(string id)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetUserData(context, id);
            }
        }


        public Users GetLoggedInUserData(string IdentityId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetLoggedInUserData(context, IdentityId);
            }
        }


    }
}
