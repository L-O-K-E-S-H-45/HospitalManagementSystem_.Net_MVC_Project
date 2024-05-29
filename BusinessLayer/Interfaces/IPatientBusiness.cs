using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPatientBusiness
    {
        bool RegisterPatient(Patient patein);
        List<Patient> GetAllPatients();
        Patient GetPatientById(int patientId);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(int patientId);
        Patient PatientLogin(LoginModel loginModel);
    }
}
