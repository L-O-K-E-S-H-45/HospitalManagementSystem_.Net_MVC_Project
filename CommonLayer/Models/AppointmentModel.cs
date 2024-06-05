using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; } 
        public int PatientId { get; set; } 
        public int DoctorId { get; set; } 
        public string ConcernAbout { get; set; } 
        public DateTime AppointmentDate { get; set; } 
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 
    }
}
