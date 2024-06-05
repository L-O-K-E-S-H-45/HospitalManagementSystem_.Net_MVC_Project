using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem_MVCProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentBusiness appointmentBusiness;
        public AppointmentController(IAppointmentBusiness appointmentBusiness)
        {
            this.appointmentBusiness = appointmentBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BookAppointment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookAppointment(AppointmentModel appointmentModel)
        {
            // Below both will work
            if (appointmentModel.PatientId == 0 || appointmentModel.DoctorId == 0) return NotFound("NotFound: Please provide required id's");
            //if (appointmentModel.PatientId == 0 || appointmentModel.DoctorId == 0) return BadRequest("BadRequest: Please provide required id's");

            // Below both will not work b/z comparing int to null
            //if (appointmentModel.PatientId == null || appointmentModel.DoctorId == null) return BadRequest("BadRequest: Please provide required id's");
            //if (appointmentModel.PatientId == null || appointmentModel.DoctorId == null) return NotFound("NotFound: Please provide required id's");

            if (appointmentModel.ConcernAbout == null) return BadRequest("Please provide ConcernAbout");

            bool result = appointmentBusiness.BookAppointment(appointmentModel);
            if (!result) 
                return NotFound("Failed to book appointment");
            return RedirectToAction("GetAllAppointments");
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            List<AppointmentModel> appointments = appointmentBusiness.GetAllAppointments().ToList();
            if (appointments == null) return NotFound("Failed to find appointments list");
            return View(appointments);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var appointments = appointmentBusiness.GetAllAppointments().ToList();
            if (appointments == null) return NotFound("Failed to find appointments list");
            return View(appointments);
        }


        [HttpGet]
        public IActionResult PatientDoctorDetails()
        {
            List<AppointmentPatientModel> result = appointmentBusiness.PatientDetailsWithAppointedDoctor();
            return View(result);
        }


    }
}
