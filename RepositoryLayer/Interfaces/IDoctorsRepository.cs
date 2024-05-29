using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IDoctorsRepository
    {
        bool AddDoctor(Doctor doctor);
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int doctorID);

        bool UpadateDoctor(Doctor doctor);
        bool DeleteDoctor(int doctorID);

    }
}
