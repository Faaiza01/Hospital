﻿using Hospital.Data;
using Hospital.Data.Models.Domain;
using Hospital.Models;
using Hospital.Services.IService;
using Hospital.Services.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forest.Controllers
{
    public class DoctorController : Controller
    {
        private IUserService UserService;

        public DoctorController()
        {
            UserService = new UserService();
        }
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }


        public void AddDoctor(RegisterViewModel model, ApplicationUser user)
        {
            Users Users = new Users
            {
                IdentityId = user.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                ContactNumber = model.ContactNumber,
                Role = "Doctor",
                Gender = model.Gender,
                Specialization = model.Specialization,
                ConsultancyFee = model.ConsultancyFee
            };
            UserService.AddUser(Users);
        }

        public ActionResult ViewDoctors()
        {
            ViewBag.doctors = UserService.GetListOfDoctors();

            return View(ViewBag.doctors);
        }

        //GET: EditHospital/Edit
        public ActionResult EditDoctor(int id)
        {
            Users doctors = UserService.GetDoctorData(id);
            return View(doctors);
        }

        [HttpPost]
        public ActionResult EditDoctor(int id, Users doctor)
        {
            try
            {
                UserService.EditDoctor(doctor, id);
                return RedirectToAction("ViewDoctors", "Doctor");
            }
            catch
            {
                return View();
            }
        }

        //// GET: DeleteHospital/Delete/5
        public ActionResult DeleteDoctor(int id)
        {
            Users doctor =  UserService.GetDoctorData(id);
            return View(doctor);
        }

        //// POST: DeleteHospital/Delete/5
        [HttpPost]
        public ActionResult DeleteDoctor(int id, Users doctor)
        {
            try
            {
                UserService.DeleteDoctor(id);
                return RedirectToAction("ViewDoctors", new { Controller = "Doctor" });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DoctorAppointments()
        {
            var identityId = User.Identity.GetUserId();
            var user = UserService.GetUserData(identityId);
            ViewBag.DocAppointments = UserService.GetDoctorAppointmentHistory(user.UserId);
            return View(ViewBag.DocAppointments);
        }

        public ActionResult DoctorCancelAppointment(int appointmentId)
        {
            UserService.CancelAppointmentByDoctor(appointmentId);
            return RedirectToAction("DoctorAppointments");
        }

        public ActionResult Treatment(int id)
        {
            TreatmentDto prescribeDto = new TreatmentDto();
            prescribeDto.AppointmentId = id;
            return View(prescribeDto);
        }

        [HttpPost]
        public ActionResult Treatment(int id, TreatmentDto prescribeDto)
        {
            try
            {
                UserService.Treatment(prescribeDto, id);
                return RedirectToAction("Index", "Treatment");
            }
            catch
            {
                return View();
            }
        }
    }
}