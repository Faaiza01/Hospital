using Job.Data.Models.Domain;
using Job.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Job.Data.IDAO
{
    public interface IJobDAO
    {
        List<Users> GetDoctorByDepartment(JobContext context);
        List<Users> GetDoctorByDepartment(JobContext context, string department);
        decimal GetDoctorsFee(JobContext context, string userId);
        void BookAppointment(JobContext context, Appointment appointment);
        Employer GetJob(JobContext context, int id);
        IList<Employer> GetJobs(JobContext context);
        void AddJob(JobContext context, Employer employer);
        void EditJob(JobContext context, Employer employer, int jobId);
        void DeleteJob(JobContext context, int id);
        void ApplyJob(JobContext context, AppliedJobs appliedJobs);
        List<AppliedJobsList> GetAppliedJobs(JobContext context, string UserId);
        List<ListOfApplicantsDto> GetListOfApplicants(JobContext context);
        void SaveJob(JobContext context, SavedJobs savedJobs);
        List<AppliedJobsList> SearchByJobTitle(JobContext context, string text, string UserId);

    }
}
