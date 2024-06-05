using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IAppointmentRepository appointmentRepository;
        public AppointmentBusiness(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public bool BookAppointment(AppointmentModel appointment)
        {
            return appointmentRepository.BookAppointment(appointment);
        }
        public List<AppointmentModel> GetAllAppointments()
        {
            return appointmentRepository.GetAllAppointments();
        }

        public List<AppointmentPatientModel> PatientDetailsWithAppointedDoctor()
        {
            return appointmentRepository.PatientDetailsWithAppointedDoctor();
        }
    }
}
