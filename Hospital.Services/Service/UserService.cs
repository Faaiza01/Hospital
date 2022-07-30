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

        public IList<App_User> GetUsers()
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetUsers(context);
            }
        }

        public void AddUser(App_User app_User)
        {

            using (var context = new JobContext())
            {
                UserDAO.AddUser(context, app_User);//Add user
                context.SaveChanges();
            }
        }

        public void RemoveUser(string id)
        {
            using (var context = new JobContext())
            {
                UserDAO.RemoveUser(context, id);
            }
        }

        public App_User GetUserData(string id)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetUserData(context, id);
            }
        }


        public App_User GetLoggedInUserData(string IdentityId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetLoggedInUserData(context, IdentityId);
            }
        }


        public void EditProfile(EditProfileDto editProfileDto, string userId)
        {
            using (var context = new JobContext())
            {
                App_User app_User = new App_User();
                app_User.FirstName = editProfileDto.FirstName;
                app_User.LastName = editProfileDto.LastName;
                UserDAO.EditProfile(context, app_User, userId);//Update existing user profile
                context.SaveChanges();
            }
        }

        public string GetResumePath(string identityId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetResumePath(context, identityId);
            }
        }
   
        public IList<AppliedJobsList> GetUserAppliedJobs(string UserId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetUserAppliedJobs(context, UserId);
            }
        }

        public IList<SavedJobList> GetUserSavedJobs(string UserId)
        {
            using (var context = new JobContext())
            {
                return UserDAO.GetUserSavedJobs(context, UserId);
            }
        }
   


  


    }
}
