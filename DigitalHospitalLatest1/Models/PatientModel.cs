using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigitalHospitalLatest1.Models
{
    public class PatientModel
    {
        public String PatientName { get; set; }
        public String PatientEmail { get; set; }
        public String Phone { get; set; }
        public String Passward { get; set; }
        public String PatientGender { get; set; }
        public String Age { get; set; }

        public String Address { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
          
       
    }
}