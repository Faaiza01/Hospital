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
        public IList<App_User> GetUsers(JobContext context)
        {
            return context.AppUsers.ToList();
        }

        public void AddUser(JobContext context, App_User app_User)
        {
            context.AppUsers.Add(app_User);
            context.SaveChanges();
        }

        public void RemoveUser(JobContext context, string identityId)
        {
            context.AppUsers.Remove(context.AppUsers.Where(c => c.IdentityId == identityId).FirstOrDefault());
            context.SaveChanges();
        }

        public App_User GetUserData(JobContext context, string id)
        {
            return context.AppUsers.ToList().Find(b => b.IdentityId == id);
        }

        public App_User GetLoggedInUserData(JobContext context, string IdentityId)
        {
            return context.AppUsers.Where(d => d.IdentityId == IdentityId).FirstOrDefault();
        }


        public void EditProfile(JobContext context, App_User employer, string userId)
        {
            var Id = context.AppUsers.Where(x => x.IdentityId == userId).Select(d => d.UserId).FirstOrDefault();
            context.AppUsers.Find(Id).FirstName = employer.FirstName;
            context.AppUsers.Find(Id).LastName = employer.LastName;
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
