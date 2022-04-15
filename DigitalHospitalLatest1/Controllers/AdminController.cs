using DigitalHospitalLatest1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalHospitalLatest1.Controllers
{
    public class AdminController: Controller
    {
        public ActionResult AdminViewPage()
        {
            return View();
        }
        public ActionResult CabinAddViewPage()
        {
            return View();
        }
        public ActionResult StaffAddViewPage()
        {
            return View();
        }
        public ActionResult DriverAddViewPage()
        {
            return View();
        }
        public ActionResult DoctorAddViewPage()
        {
            return View();
        }
        public ActionResult AmbulanceAddViewPage()
        {
            return View();
        }
        public ActionResult Adding_Doctor(DoctorModel Doctor_info)
        {
            int affectedRow;
            string email, phone,error;
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            PatientModel Patient = new PatientModel();
            SqlConnection connection2 = new SqlConnection(myConnectionString);
            connection2.Open();
            SqlCommand com2 = new SqlCommand("select Doctor_email,Doctor_phone from Doctor", connection2);
             SqlDataReader data = com2.ExecuteReader();
                while (data.Read())
                {
                  email = data[0].ToString();
                if(email==Doctor_info.Doctor_email)
                {
                    error = "This Email already exists";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                  phone = data[1].ToString();
                if (phone == Doctor_info.Doctor_phone)
                {
                    error = "This phone number already exists";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }
            data.Close();
            connection2.Close();
            connection.Open();
            SqlCommand com = new SqlCommand("insert into Doctor values('" + Doctor_info.Doctor_name + "','" + Doctor_info.Doctor_email + "','" + Doctor_info.Doctor_phone + "','" + Doctor_info.Doctor_gender + "','" + Doctor_info.Doctor_field + "','"+Doctor_info.Doctor_salary+ "','"+Doctor_info.Doctor_password+ "','"+Doctor_info.Doctor_age+ "','"+Doctor_info.Doctor_address+ "')", connection);
            affectedRow = com.ExecuteNonQuery();
            // var count = (string)com.ExecuteScalar();
            connection.Close();

            SqlConnection connection1 = new SqlConnection(myConnectionString);
            connection1.Open();
            SqlCommand com1 = new SqlCommand("insert into tbl_User values('" + Doctor_info.Doctor_phone + "','Doctor')", connection1);
            var count1 = com1.ExecuteNonQuery();
            connection1.Close();
            if (affectedRow == 1)
            {
                return Json(affectedRow, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(affectedRow, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult saveDriver(AdminModel Driver_info)
        {
            
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("insert into Tbl_Driver values('" + Driver_info.Driver_name+ "','" + Driver_info.Driver_phone + "','" + Driver_info.Driver_age +"')", connection);
            int affectedRow = com.ExecuteNonQuery();
            // var count = (string)com.ExecuteScalar();
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
        public ActionResult saveStaff(AdminModel Staff_info)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("insert into Tbl_Staff values('"+Staff_info.staff_name+"','"+Staff_info.staff_phone+"','"+Staff_info.staff_age+ "','"+Staff_info.staff_type+ "')", connection);
            int affectedRow = com.ExecuteNonQuery();
            // var count = (string)com.ExecuteScalar();
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
        public ActionResult saveCabin(AdminModel Cabin_info)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("insert into Tbl_Cabin (CabinType,Cabin_no) values ('"+Cabin_info.CabinType+"','"+Cabin_info.Cabin_no+"')", connection);
            int affectedRow = com.ExecuteNonQuery();
            // var count = (string)com.ExecuteScalar();
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

        public ActionResult AssignStaffToCabin(AdminModel Staff_Assigning)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Tbl_Cabin set staff_id="+Staff_Assigning.staff_id+"where CabinID=(Select top(1) CabinID from Tbl_Cabin where staff_id IS NULL ORDER BY CabinID ASC)", connection);
            int affectedRow = cmd.ExecuteNonQuery();
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
        public ActionResult saveAmbulance(AdminModel Ambulance_info)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("insert into Tbl_Ambulance (AmbulanceType,AmbulanceNo) values ('" + Ambulance_info.AmbulanceType + "','" + Ambulance_info.AmbulanceNo + "')", connection);
            int affectedRow = com.ExecuteNonQuery();
            // var count = (string)com.ExecuteScalar();
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
        public ActionResult Getstaff()
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);

            List<AdminModel> doctors = new List<AdminModel>();
            connection.Open();

            SqlCommand cmd = new SqlCommand("select * from Tbl_Staff", connection);

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable

            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AdminModel aStaff = new AdminModel();
                aStaff.staff_id = row["staff_id"].ToString();
                aStaff.staff_name = row["staff_name"].ToString();
                aStaff.staff_phone = row["staff_phone"].ToString();
                aStaff.staff_age = row["staff_age"].ToString();
                aStaff.staff_type = row["staff_type"].ToString();
                aStaff.staff_availity = row["staff_availity"].ToString();

                doctors.Add(aStaff);
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