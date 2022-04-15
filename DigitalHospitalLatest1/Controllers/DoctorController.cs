using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalHospitalLatest1.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DigitalHospitalLatest1.Controllers
{
    public class DoctorController:Controller
    {
        // GET: /<controller>//
        public ActionResult User_profile()
        {
            return View();
        }
       
        public ActionResult DoctorPrivatePage()
        {
            return View();
        }
        public ActionResult EditData(DoctorModel Edit_Doctor_info)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("UPDATE Doctor SET Doctor_name='" + Edit_Doctor_info.Doctor_name + "',Doctor_email='" + Edit_Doctor_info.Doctor_email + "',Doctor_phone='" + Edit_Doctor_info.Doctor_phone + "',Doctor_password='" + Edit_Doctor_info.Doctor_password + "',Doctor_gender='" + Edit_Doctor_info.Doctor_gender+ "'WHERE Doctor_phone='"+Edit_Doctor_info.Doctor_phone+"'", connection);
            int affectedRow = com.ExecuteNonQuery();

            connection.Close();
            if (affectedRow == 1)
            {
                return Json(affectedRow, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(affectedRow, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public ActionResult GetDoctorByPhone(string Doctor_phone)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            return null;
        }
        [HttpGet]
        public ActionResult GetDoctors()
        {
            DoctorModel adoctor = new DoctorModel();
            List<DoctorModel> doctors = adoctor.GetAllDoctor();

            var jsonString = JsonConvert.SerializeObject(new
            {
                doctors
            });

            return Json(new { data = doctors }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]         
        public ActionResult GetAllDoctors(DoctorModel Doctor_field)
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
                adoctor.Doctor_name = row["Doctor_name"].ToString();
                adoctor.Doctor_email = row["Doctor_email"].ToString();
                adoctor.Doctor_phone = row["Doctor_phone"].ToString();
                adoctor.Doctor_gender = row["Doctor_gender"].ToString();
                adoctor.Doctor_field = row["Doctor_field"].ToString();
                adoctor.Doctor_age = row["Doctor_age"].ToString();

                doctors.Add(adoctor);
            }           

            connection.Close();
            var jsonString = JsonConvert.SerializeObject(new
            {
                doctors
            });

            return Json(new { data = doctors }, JsonRequestBehavior.AllowGet);

        }
    }
}