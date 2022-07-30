using Job.Data.IDAO;
using Job.Data.Models.Domain;
using Job.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Job.Data.DAO
{
    public class JobDAO : IJobDAO
    {

        public Employer GetJob(JobContext context, int id)
        {
            return context.Employers.ToList().Find(b => b.JobId == id);
        }

        public IList<Employer> GetJobs(JobContext context)
        {
            return context.Employers.ToList();
        }

        public void AddJob(JobContext context, Employer employer)
        {
            context.Employers.Add(employer);
            context.SaveChanges();
        }


        public void SaveJob(JobContext context,SavedJobs savedJobs)
        {
            context.SavedJobs.Add(savedJobs);
            context.SaveChanges();
        }

        public void ApplyJob(JobContext context, AppliedJobs appliedJobs)
        {
            context.AppliedJobs.Add(appliedJobs);
            context.SaveChanges();
        }

        public void EditJob(JobContext context, Employer employer, int jobId)
        {
            context.Employers.Find(jobId).JobTitle = employer.JobTitle;
            context.Employers.Find(jobId).JobDescription = employer.JobDescription;
            context.Employers.Find(jobId).JobCategory = employer.JobCategory;
            context.Employers.Find(jobId).Salary = employer.Salary;
            context.Employers.Find(jobId).CompanyName = employer.CompanyName;
            context.Employers.Find(jobId).CompanyAddress = employer.CompanyAddress;
            context.Employers.Find(jobId).ComapanyEmail = employer.ComapanyEmail;
            context.Employers.Find(jobId).CompanyWebsite = employer.CompanyWebsite;
            context.SaveChanges();
        }

        public void DeleteJob(JobContext context, int id)
        {
            var appliedJobs = context.AppliedJobs.Where(x => x.JobId == id).ToList();
            foreach (var item in appliedJobs)
            {
                context.AppliedJobs.Remove(item);
            }
            var savedJobs = context.SavedJobs.Where(x => x.JobId == id).ToList();
            foreach (var item in savedJobs)
            {
                context.SavedJobs.Remove(item);
            }
            context.Employers.Remove(context.Employers.Find(id));
            context.SaveChanges();
        }
        public List<AppliedJobsList> GetAppliedJobs(JobContext context, string UserId)
        {
            List<AppliedJobsList> appliedJobsList = new List<AppliedJobsList>();
            var result = context.Employers.ToList();
            foreach (var item in result)
            {
                AppliedJobsList appliedJobsList1 = new AppliedJobsList();
                appliedJobsList1.JobId = item.JobId;
                appliedJobsList1.JobTitle = item.JobTitle;
                appliedJobsList1.JobDescription = item.JobDescription;
                appliedJobsList1.JobCategory = item.JobCategory;
                appliedJobsList1.Salary = item.Salary;
                appliedJobsList1.CompanyName = item.CompanyName;
                appliedJobsList1.ComapanyEmail = item.ComapanyEmail;
                appliedJobsList1.CompanyAddress = item.CompanyAddress;
                appliedJobsList1.CompanyWebsite = item.CompanyWebsite;
                appliedJobsList1.IsApplied = IsApplied(context, item.JobId, UserId);
                appliedJobsList1.IsSaved = IsSaved(context, item.JobId, UserId);
                appliedJobsList1.AppliedJobId = context.AppliedJobs.Where(c => c.JobId == item.JobId && c.UserIdentityId == UserId).Select(v => v.AppliedJobId).FirstOrDefault();
                appliedJobsList.Add(appliedJobsList1);
            }
            return appliedJobsList;
        }


        public List<ListOfApplicantsDto> GetListOfApplicants(JobContext context)
        {
            List<ListOfApplicantsDto> listOfApplicantsDtos = new List<ListOfApplicantsDto>();
            var applicants = context.AppliedJobs.ToList();
            foreach (var item in applicants)
            {
                ListOfApplicantsDto listOfApplicantsDtos1 = new ListOfApplicantsDto();

                var jobs = context.Employers.Where(c => c.JobId == item.JobId).FirstOrDefault();
                var jobSeeker = context.AppUsers.Where(f => f.IdentityId == item.UserIdentityId).FirstOrDefault();
                listOfApplicantsDtos1.NameOfApplicant = jobSeeker?.FirstName + ' ' + jobSeeker?.LastName;
                listOfApplicantsDtos1.Email = jobSeeker?.Email;
                listOfApplicantsDtos1.IdentityId = jobSeeker?.IdentityId;
                listOfApplicantsDtos1.JobTitle = jobs?.JobTitle;
                listOfApplicantsDtos1.CompanyName = jobs?.CompanyName;
                listOfApplicantsDtos1.ComapanyEmail = jobs?.ComapanyEmail;
                listOfApplicantsDtos1.AppliedJobDate = item?.AppliedJobDate;
                listOfApplicantsDtos.Add(listOfApplicantsDtos1);
            }
            return listOfApplicantsDtos;
        }

        public List<AppliedJobsList> SearchByJobTitle(JobContext context, string text, string UserId)
        {
            List<AppliedJobsList> appliedJobsList = new List<AppliedJobsList>();
            var result = context.Employers.Where(p => p.JobTitle.Contains(text)).ToList();
            foreach (var item in result)
            {
                AppliedJobsList appliedJobsList1 = new AppliedJobsList();
                appliedJobsList1.JobId = item.JobId;
                appliedJobsList1.JobTitle = item.JobTitle;
                appliedJobsList1.JobDescription = item.JobDescription;
                appliedJobsList1.JobCategory = item.JobCategory;
                appliedJobsList1.Salary = item.Salary;
                appliedJobsList1.CompanyName = item.CompanyName;
                appliedJobsList1.ComapanyEmail = item.ComapanyEmail;
                appliedJobsList1.CompanyAddress = item.CompanyAddress;
                appliedJobsList1.CompanyWebsite = item.CompanyWebsite;
                appliedJobsList1.IsApplied = IsApplied(context, item.JobId, UserId);
                appliedJobsList1.IsSaved = IsSaved(context, item.JobId, UserId);
                appliedJobsList1.AppliedJobId = context.AppliedJobs.Where(c => c.JobId == item.JobId && c.UserIdentityId == UserId).Select(v => v.AppliedJobId).FirstOrDefault();
                appliedJobsList.Add(appliedJobsList1);
            }
            return appliedJobsList;
        }


        public bool IsApplied(JobContext context, int jobId, string UserId)
        {
            var result = context.AppliedJobs.ToList().Find(a => a.JobId == jobId && a.UserIdentityId == UserId);
            if (result == null)
                return false;
            else
                return true;
        }

        public bool? IsSaved(JobContext context, int jobId, string UserId)
        {
            var result = context.SavedJobs.ToList().Find(a => a.JobId == jobId && a.UserIdentityId == UserId);
            if (result == null)
                return false;
            else
                return true;
        }

    }

}
