using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DigitalHospitalLatest1.Models
{
    public class DoctorModel
    {
        public string Doctor_id { get; set; }
        public string Doctor_name { get; set; }
        public string Doctor_email { get; set; }
        public string Doctor_phone { get; set; }
        public string Doctor_gender { get; set; }
        public string Doctor_field { get; set; }
        public string Doctor_salary { get; set; }
        public string Doctor_password { get; set; }
        public string Doctor_age { get; set; }
        public string Doctor_address { get; set; }

        internal List<DoctorModel> GetAllDoctor()
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);

            List<DoctorModel> doctors = new List<DoctorModel>();
            connection.Open();

            SqlCommand cmd = new SqlCommand("select * from Doctor", connection);

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable

            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DoctorModel adoctor = new DoctorModel();
                adoctor.Doctor_id = row["Doctor_id"].ToString();
                adoctor.Doctor_name = row["Doctor_name"].ToString();
                adoctor.Doctor_email = row["Doctor_email"].ToString();
                adoctor.Doctor_phone = row["Doctor_phone"].ToString();
                adoctor.Doctor_gender = row["Doctor_gender"].ToString();
                adoctor.Doctor_field = row["Doctor_field"].ToString();
                adoctor.Doctor_salary = row["Doctor_salary"].ToString();
                adoctor.Doctor_age = row["Doctor_age"].ToString();
                adoctor.Doctor_address = row["Doctor_address"].ToString();

                doctors.Add(adoctor);
            }

            connection.Close();
            return doctors;
        }
    }
}