using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem_MVCProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorBusiness doctorBusiness;
        public DoctorController(IDoctorBusiness doctorBusiness)
        {
            this.doctorBusiness = doctorBusiness;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            var res = doctorBusiness.AddDoctor(doctor);
            if (res)
                return RedirectToAction("GetAllDoctors");
            return View(doctor);
        }


        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            List<Doctor> doctorsList = new List<Doctor>();
            doctorsList = doctorBusiness.GetAllDoctors().ToList();
            if (doctorsList != null)
            {
                //foreach(Doctor doctor in doctorsList)
                //{
                //    int DoctorId = doctor.DoctorId;
                //    HttpContext.Session.SetInt32("DoctorId", DoctorId);
                //}
                return View(doctorsList);
            }
            return View("Index");
        }

        [HttpGet]
        //[Route("GetById/{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            if (doctorId != null)
            {
                Doctor doctor = doctorBusiness.GetDoctorById(doctorId);
                if (doctor != null)
                {
                    HttpContext.Session.SetInt32("DoctorId", doctor.DoctorId);
                    return View(doctor);
                }
                else
                    return NotFound("Doctor not found for requested id: " + doctorId);
                //return NotFound();
            }
            else
                return NotFound();
        }

        [HttpGet]
        //[Route("UpdateById/{id}")]
        public IActionResult UpdateDoctor(int id)
        {
            if (id == null)
                return NotFound("Please provide doctorId");
            Doctor doctor = doctorBusiness.GetDoctorById(id);
            if (doctor == null)
                return NotFound("Doctor not found for requested id: " + id);
            return View(doctor);
        }

        [HttpPost]
        public IActionResult UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
                return NotFound("Mismatch id");
            bool result = doctorBusiness.UpadateDoctor(doctor);
            if (result)
                return RedirectToAction("GetAllDoctors");
            return View(doctor);
        }

        [HttpGet]
        public IActionResult DeleteDoctor(int id)
        {
            if (id == null || id == 0)
                return NotFound("Please pass id");
            Doctor doctor = doctorBusiness.GetDoctorById(id);
            if (doctor == null)
                return NotFound("Doctor not found ford requested id: " + id);
            return View(doctor);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeleteDoctor(int id)
        {
            if (id == 0)
                return View("index");
            bool result = doctorBusiness.DeleteDoctor(id);

            if (result) return RedirectToAction("GetAllDoctors");
            return NotFound("Failed to delet");
        }

        [HttpGet]
        public IActionResult DoctorLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoctorLogin(LoginModel loginModel)
        {
            var doctor = doctorBusiness.DoctorLogin(loginModel);
            if (doctor != null)
            {
                HttpContext.Session.SetInt32("DoctorId", doctor.DoctorId);
                return RedirectToAction("GetDoctorById", new { doctorId = doctor.DoctorId });
            }
            return NotFound("Failed to login b/z invalid credentials");
        }


    }
}
