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
    public class DoctorBusiness : IDoctorBusiness
    {
        private readonly IDoctorsRepository doctorsRepository;
        public DoctorBusiness(IDoctorsRepository doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }
        public bool AddDoctor(Doctor doctor)
        {
           return doctorsRepository.AddDoctor(doctor);
        }


        public List<Doctor> GetAllDoctors()
        {
            return doctorsRepository.GetAllDoctors();
        }

        public Doctor GetDoctorById(int doctorID)
        {
            return doctorsRepository.GetDoctorById(doctorID);
        }

        public bool UpadateDoctor(Doctor doctor)
        {
            return doctorsRepository.UpadateDoctor(doctor);
        }
        public bool DeleteDoctor(int doctorID)
        {
            return doctorsRepository.DeleteDoctor(doctorID);
        }
    }
}
