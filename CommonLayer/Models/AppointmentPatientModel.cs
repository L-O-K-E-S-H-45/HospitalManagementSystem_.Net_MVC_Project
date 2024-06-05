using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class AppointmentPatientModel    
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public long Contact { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PatientImage { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }
    }
}
