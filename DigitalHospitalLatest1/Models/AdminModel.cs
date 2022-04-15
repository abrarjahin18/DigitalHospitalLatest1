using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHospitalLatest1.Models
{
    public class AdminModel
    {
        public string Driver_name { get; set; }

        public string Driver_phone { get; set; }

        public string Driver_age { get; set; }

        public string  staff_id { get; set; }
        public string staff_name { get; set; }

        public string staff_phone { get; set; }

        public string staff_age { get; set; }

        public string staff_type { get; set; }

        public string  staff_availity{ get; set; }

        public string Cabin_no { get; set; }

        public string CabinType { get; set; }
        public string AmbulanceNo { get; set; }
        public string AmbulanceType { get; set; }
    }
}