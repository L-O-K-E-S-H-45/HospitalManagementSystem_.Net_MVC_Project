using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Contact {  get; set; }
        public DateTime DOB { get; set; }
        public Decimal Age { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
        public string DoctorImage { get; set; }
        public bool IsTrash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
