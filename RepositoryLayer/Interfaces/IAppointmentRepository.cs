using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAppointmentRepository
    {
        bool BookAppointment(AppointmentModel appointment);
        List<AppointmentModel> GetAllAppointments();

        List<AppointmentPatientModel> PatientDetailsWithAppointedDoctor();
    }
}
