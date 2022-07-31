﻿using Job.Data;
using Job.Data.DAO;
using Job.Data.IDAO;
using Job.Data.Models.Domain;
using Job.Data.Repository;
using Job.Services.IService;
using Job.Services.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Job.Services.Service
{
    public class JobService : IJobService
    {
        private IJobDAO JobDAO;
  
        public JobService()
        {
            JobDAO = new JobDAO();
        }

        public List<Users> GetDoctorByDepartment()
        {
            using (var context = new JobContext())
            {
               return JobDAO.GetDoctorByDepartment(context);
            }
        }

        public List<Users> GetDoctorByDepartment(string department)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetDoctorByDepartment(context, department);
            }
        }

        public decimal GetDoctorsFee(string userId)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetDoctorsFee(context, userId);
            }
        }

        public void BookAppointment(BookAppointmentDto bookAppointmentDto, int userId)
        {
            Appointment appointment = new Appointment()
            {
                PatientId = userId,
                DoctorId = Int32.Parse(bookAppointmentDto.Doctor),
                AppointmentDateTime = bookAppointmentDto.AppointmentDateTime,
                Status = true
            };

            using (var context = new JobContext())
            {
                JobDAO.BookAppointment(context, appointment);//Add job
                context.SaveChanges();
            }
        }

        public IList<PAppointmentHistoryDto> GetPatientAppointmentHistory(int userId)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetPatientAppointmentHistory(context, userId);
            }
        }

        public void CancelAppointment(int appointmentId)
        {
            using (var context = new JobContext())
            {
                JobDAO.CancelAppointment(context, appointmentId);
            }
        }

        public IList<AppointmentDetailsDto> AppointmentDetail()
        {
            using (var context = new JobContext())
            {
                return JobDAO.AppointmentDetail(context);
            }
        }
        public Employer GetJob(int id)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetJob(context, id);
            }
        }

        public IList<Employer> GetJobs()
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetJobs(context);
            }
        }

        public void AddJob(PostJobDto postJobDto, string userId)
        {
            Employer newEmployer = new Employer()
            {
                JobTitle = postJobDto.JobTitle,
                JobDescription = postJobDto.JobDescription,
                JobCategory = postJobDto.JobCategory,
                Salary = postJobDto.Salary,
                CompanyName = postJobDto.CompanyName,
                CompanyAddress = postJobDto.CompanyAddress,
                ComapanyEmail = postJobDto.ComapanyEmail,
                CompanyWebsite = postJobDto.CompanyWebsite
            };

            using (var context = new JobContext())
            {
                JobDAO.AddJob(context, newEmployer);//Add job
                context.SaveChanges();
            }
        }

        public void EditJob(PostJobDto postJobDto, string userId, int id)
        {
            using (var context = new JobContext())
            {
                Employer employer = new Employer();
                employer.JobTitle = postJobDto.JobTitle;
                employer.JobDescription = postJobDto.JobDescription;
                employer.JobCategory = postJobDto.JobCategory;
                employer.Salary = postJobDto.Salary;
                employer.CompanyName = postJobDto.CompanyName;
                employer.CompanyAddress = postJobDto.CompanyAddress;
                employer.ComapanyEmail = postJobDto.ComapanyEmail;
                employer.CompanyWebsite = postJobDto.CompanyWebsite;

                JobDAO.EditJob(context, employer, id);//Update existing job         
                context.SaveChanges();
            }
        }

        public void DeleteJob(int id)
        {
            using (var context = new JobContext())
            {
                JobDAO.DeleteJob(context, id);
            }
        }
     
        public void ApplyJob(AppliedJobs appliedJobs)
        {         
            using (var context = new JobContext())
            {   
                JobDAO.ApplyJob(context, appliedJobs);//Apply for a job
                context.SaveChanges();
            }   
        }


        public IList<AppliedJobsList> GetAppliedJobs(string UserId)
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetAppliedJobs(context, UserId);
            }
        }

        public IList<ListOfApplicantsDto> GetListOfApplicants()
        {
            using (var context = new JobContext())
            {
                return JobDAO.GetListOfApplicants(context);
            }
        }

        public void SaveJob(SavedJobs savedJobs)
        {
            using (var context = new JobContext())
            {
                JobDAO.SaveJob(context, savedJobs);
            }
        }

        public IList<AppliedJobsList> SearchByJobTitle(string text, string userId)
        {
            using (var context = new JobContext())
            {
                return JobDAO.SearchByJobTitle(context, text, userId);
            }
        }

 


    }
}
