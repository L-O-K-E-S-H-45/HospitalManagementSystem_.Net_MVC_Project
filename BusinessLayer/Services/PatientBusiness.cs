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
    public class PatientBusiness : IPatientBusiness
    {
        private readonly IPatientRepository patientRepository;
        public PatientBusiness(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public bool RegisterPatient(Patient patein)
        {
            return patientRepository.RegisterPatient(patein);
        }

        public List<Patient> GetAllPatients()
        {
            return patientRepository.GetAllPatients();
        }

        public Patient GetPatientById(int patientId)
        {
            return patientRepository.GetPatientById(patientId);
        }

        public bool UpdatePatient(Patient patient)
        {
            return patientRepository.UpdatePatient(patient);
        }

        public bool DeletePatient(int patientId)
        {
            return patientRepository.DeletePatient(patientId);
        }

        public Patient PatientLogin(LoginModel loginModel)
        {
            return patientRepository.PatientLogin(loginModel);
        }
    }
}
