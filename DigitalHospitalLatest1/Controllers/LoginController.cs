using DigitalHospitalLatest1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalHospitalLatest1.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult LoginView()
        {
            if (Session["PatientName"] != null)
            {
                Session["PatientName"] = null;
            }
            if(Session["Doctor_name"] !=null)
            {
                Session["Doctor_name"] = null;
            }
            return View();
        }
        [HttpPost]
        public ActionResult login_user(LoginModel Login_info)
        {
                string passward = null;
                string patientName = null;
            string DoctorName = null;
            string Doctor_passward = null;
            string status = null;
            string user_type = null;
            int Patientid=0;
                 String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
                SqlConnection connection = new SqlConnection(myConnectionString);
                connection.Open();
                SqlCommand com = new SqlCommand("select Passward,PatientName,Patient_id from patient where Phone ='" + Login_info.Phone + "'", connection);
            //SqlCommand com = new SqlCommand("select Passward,userType from user where Phone ='" + Login_info.Phone + "'", connection);
            SqlDataReader data = com.ExecuteReader();
                while (data.Read())
                {
                    passward = data[0].ToString();
                    patientName = data[1].ToString();
                    Patientid = (int)data[2];
            }
            data.Close();
                connection.Close();
            SqlConnection connection1 = new SqlConnection(myConnectionString);
            connection1.Open();
            SqlCommand com1 = new SqlCommand("select user_type from tbl_User where user_phone ='" + Login_info.Phone + "'", connection1);
            SqlDataReader data1 = com1.ExecuteReader();
            while (data1.Read())
            {
                user_type = data1[0].ToString();
            }
            data1.Close();
            connection1.Close();
            SqlConnection connection2 = new SqlConnection(myConnectionString);
            connection2.Open();
            SqlCommand com2 = new SqlCommand("select Doctor_password, Doctor_name from Doctor where Doctor_phone = '" + Login_info.Phone + "'", connection2);
            SqlDataReader data2 = com2.ExecuteReader();
            while (data2.Read())
            {
                Doctor_passward = data2[0].ToString();
                DoctorName = data2[1].ToString();
            }
            data2.Close();
            connection2.Close();

            if (user_type=="Patient" && Login_info.Passward == passward)
            {
                status = "patient";
                Session["PatientName"] = patientName;
                Session["Patient_id"] = Patientid;

            }
            if (user_type == "Patient" && Login_info.Passward != passward)
            {
                status = "Password is incorrect";
                
            }
            if (user_type=="Doctor"&&Login_info.Passward== Doctor_passward)
            {
                status = "doctor";
                Session["Doctor_name"] = DoctorName;
            }
            if(Login_info.Phone=="01700000000"&&Login_info.Passward=="admin")
            {
                status = "admin";
            }
            if(user_type!= "Patient"&& user_type!="Doctor"&& user_type != "Admin")
            {
                status = "none";

            }
            return Json(status, JsonRequestBehavior.AllowGet);
            }
        //}
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("PatientSignupView", "Patient");
            }
        }

        
    }
}