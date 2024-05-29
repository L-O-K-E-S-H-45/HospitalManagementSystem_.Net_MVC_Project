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

    }
}
