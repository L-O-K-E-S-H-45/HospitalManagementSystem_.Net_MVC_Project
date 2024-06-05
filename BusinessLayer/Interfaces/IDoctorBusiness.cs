using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDoctorBusiness
    {
        bool AddDoctor(Doctor doctor);
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int doctorID);
        bool UpadateDoctor(Doctor doctor);
        bool DeleteDoctor(int doctorID);
        Doctor DoctorLogin(LoginModel loginModel);
    }
}
