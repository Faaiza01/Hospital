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
        IList<App_User> GetUsers(JobContext context);
        void AddUser(JobContext context, App_User app_User);
        void RemoveUser(JobContext context, string identityId);
        App_User GetUserData(JobContext context, string id);
        App_User GetLoggedInUserData(JobContext context, string IdentityId);
        void EditProfile(JobContext context, App_User employer, string userId);
        string GetResumePath(JobContext context, string IdentityId);
        List<AppliedJobsList> GetUserAppliedJobs(JobContext context, string UserId);
        List<SavedJobList> GetUserSavedJobs(JobContext context, string UserId);
    }
}
