using Job.Data;
using Job.Data.Models.Domain;
using Job.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.IService
{
    public interface IJobService
    {
        Employer GetJob(int id);
        IList<Employer> GetJobs();
        void AddJob(PostJobDto postJobDto, string userId);
        void EditJob(PostJobDto postJobDto, string userId, int id);
        void DeleteJob(int id);
        void ApplyJob(AppliedJobs appliedJobs);
        IList<AppliedJobsList> GetAppliedJobs(string UserId);
        IList<ListOfApplicantsDto> GetListOfApplicants();
        void SaveJob(SavedJobs savedJobs);
        IList<AppliedJobsList> SearchByJobTitle(string text, string userId);


    }
}
