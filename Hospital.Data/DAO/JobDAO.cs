﻿using Job.Data.IDAO;
using Job.Data.Models.Domain;
using Job.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace Job.Data.DAO
{
    public class JobDAO : IJobDAO
    {
        public List<Users> GetDoctorByDepartment(JobContext context)
        {           
            //.Select(m => new SelectListItem { Text = m.FirstName, Value = m.UserId.ToString() })
            var result=   context.Users.ToList();
            return result;
        }

        public List<Users> GetDoctorByDepartment(JobContext context, string department)
        {
            //.Select(m => new SelectListItem { Text = m.FirstName, Value = m.UserId.ToString() })
            var result = context.Users.Where(x => x.Specialization== department).ToList();
            return result;
        }

        public decimal GetDoctorsFee(JobContext context, string userId)
        {
            int id = Int32.Parse(userId);
            return context.Users.Where(x => x.UserId == id).FirstOrDefault().ConsultancyFee;
          
        }

        public void BookAppointment(JobContext context, Appointment appointment)
        {
            context.Appointment.Add(appointment);
            context.SaveChanges();
        }

        public List<PAppointmentHistoryDto> GetPatientAppointmentHistory(JobContext context, int userId)
        {
            List<PAppointmentHistoryDto> appointmentHistoryDtos = new List<PAppointmentHistoryDto>();
            var myAppointments = context.Appointment.Where(x => x.PatientId == userId).ToList();
            foreach (var item in myAppointments)
            {
                PAppointmentHistoryDto pAppointmentHistoryDto = new PAppointmentHistoryDto();

                var myDoctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                pAppointmentHistoryDto.AppointmentId = item.AppointmentId;
                pAppointmentHistoryDto.DoctorName = myDoctor?.FirstName + ' ' + myDoctor?.LastName;
                pAppointmentHistoryDto.ConsultancyFee = myDoctor?.ConsultancyFee;
                pAppointmentHistoryDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                pAppointmentHistoryDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                pAppointmentHistoryDto.Status = item?.Status ==  true ? "Active" : "Closed";
                pAppointmentHistoryDto.IsCancelledByPatient = item.IsCancelledByPatient;
                pAppointmentHistoryDto.IsCancelledByDoctor = item.IsCancelledByDoctor;
                appointmentHistoryDtos.Add(pAppointmentHistoryDto);
            }
            return appointmentHistoryDtos;
        }

        public void CancelAppointment(JobContext context, int appointmentId)
        {
            context.Appointment.Find(appointmentId).IsCancelledByPatient = true;
            context.Appointment.Find(appointmentId).Status = false;
            context.SaveChanges();
        }

        public List<AppointmentDetailsDto> AppointmentDetail(JobContext context)
        {
            List<AppointmentDetailsDto> appointmentDetailsDtos = new List<AppointmentDetailsDto>();
            var appointments = context.Appointment.ToList();
            foreach (var item in appointments)
            {
                AppointmentDetailsDto appointmentDetailsDto = new AppointmentDetailsDto();

                var doctor = context.Users.Where(c => c.UserId == item.DoctorId).FirstOrDefault();
                var patient = context.Users.Where(c => c.UserId == item.PatientId).FirstOrDefault();

                appointmentDetailsDto.DoctorName = doctor?.FirstName + ' ' + doctor?.LastName;
                appointmentDetailsDto.ConsultancyFee = doctor?.ConsultancyFee;
                appointmentDetailsDto.PatientName = patient?.FirstName + ' ' + patient?.LastName;
                appointmentDetailsDto.Gender = patient?.Gender;
                appointmentDetailsDto.ContactNumber = patient?.ContactNumber;
                appointmentDetailsDto.AppointmentDate = item?.AppointmentDateTime.Split(' ')[0];
                appointmentDetailsDto.AppointmentTime = item?.AppointmentDateTime.Split(' ')[1];
                appointmentDetailsDto.Status = item?.Status == true ? "Active" : "Closed";
                appointmentDetailsDtos.Add(appointmentDetailsDto);
            }
            return appointmentDetailsDtos;
        }

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
                var jobSeeker = context.Users.Where(f => f.IdentityId == item.UserIdentityId).FirstOrDefault();
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
