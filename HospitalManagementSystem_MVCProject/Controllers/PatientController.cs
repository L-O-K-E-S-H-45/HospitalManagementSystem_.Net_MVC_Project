using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem_MVCProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientBusiness patientBusiness;
        public PatientController(IPatientBusiness patientBusiness)
        {
            this.patientBusiness = patientBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPatient(Patient patient)
        {
            var result = patientBusiness.RegisterPatient(patient);
            if (result)
                return RedirectToAction("GetAllPatients");
            return View(patient);
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = patientBusiness.GetAllPatients().ToList();
            return View(patients);
        }

        [HttpGet]
        public IActionResult GetPatientById(int patientId)
        {
            if (patientId == null || patientId == 0)
                return NotFound("Patient id may be null or zero");
            Patient patient = patientBusiness.GetPatientById(patientId);
            if (patient == null)
                return NotFound("Patient not found for requested id: " + patientId);
            return View(patient);
        }

        [HttpGet]
        public IActionResult UpdatePatient(int id)
        {
            if (id == null || id == 0)
                return NotFound("Patient id may be null or zero, id: " + id);
            Patient patient = patientBusiness.GetPatientById(id);
            if (patient == null)
                return NotFound("Patient not found for id: " + id);
            return View(patient);
        }

        [HttpPost]
        public IActionResult UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
                return NotFound("Missmatch id");
            bool result = patientBusiness.UpdatePatient(patient);
            if (!result) return NotFound("Failed to update patient");
            else return RedirectToAction("GetAllPatients"); 
        }

        [HttpGet]
        public IActionResult DeletePatient(int id)
        {
            if (id == null || id == 0)
                return NotFound("Patient id may be null or zero, id: " + id);
            Patient patient = patientBusiness.GetPatientById(id);
            if (patient == null)
                return NotFound("Patient not found for id: " + id);
            return View(patient);
        }

        [HttpPost, ActionName("DeletePatient")]
        public IActionResult ConfirmDeletePatient(int id)
        {
            bool result = patientBusiness.DeletePatient(id);
            if (!result) return NotFound("Failed to delete patient");
            else return RedirectToAction("GetAllPatients");
        }

    }
}
