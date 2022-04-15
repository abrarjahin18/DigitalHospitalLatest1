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
    public class PatientController: Controller
    {

       
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CabinBookingViewPage()
        {
            return View();
        }
        public ActionResult User_Profile()
        {
            return View();
        }
        public ActionResult PatientSignupView()
        {
            return View();
        }
        public ActionResult PatientPrivatePage()
        {
            return View();
        }

        [HttpPost] 
       public ActionResult savePatient(PatientModel Patient_info)
        {
            string email, phone,error;
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;

            SqlConnection connection2 = new SqlConnection(myConnectionString);
            connection2.Open();
            SqlCommand com2= new SqlCommand("select PatientEmail,Phone from patient", connection2);
            SqlDataReader data = com2.ExecuteReader();
            while (data.Read())
            {
                email = data[0].ToString();
                if (email == Patient_info.PatientEmail)
                {
                    error = "This Email already exists";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                phone = data[1].ToString();
                if (phone == Patient_info.Phone)
                {
                    error = "This phone number already exists";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }
            SqlConnection connection = new SqlConnection(myConnectionString);
            PatientModel Patient = new PatientModel();
            connection.Open();
            SqlCommand com = new SqlCommand("insert into patient values('"+ Patient_info.PatientName+"','"+ Patient_info.PatientEmail + "','"+ Patient_info.Phone+ "','"+ Patient_info.Passward+ "','"+ Patient_info.PatientGender+ "','" + Patient_info.Age + "','" + Patient_info.Address + "')", connection);
             int count = com.ExecuteNonQuery();
            // var count = (string)com.ExecuteScalar();
            
            connection.Close();

            SqlConnection connection1 = new SqlConnection(myConnectionString);
            connection1.Open();
            SqlCommand com1 = new SqlCommand("insert into tbl_User values('" + Patient_info.Phone + "','Patient')", connection1);
            var count1 = com1.ExecuteNonQuery();
            connection1.Close();
            if (count == 1)
            {
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(count, JsonRequestBehavior.AllowGet);

            }
            //return null;
        }

        public ActionResult BookCabin(AdminModel cabin_booking)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE  Tbl_Cabin SET Patient_id=(select Patient_id from patient where PatientName='"+ Session["PatientName"] + "' AND Patient_id=" + Session["Patient_id"]+") where Cabin_no='" + cabin_booking.Cabin_no+ "'", connection);
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
        public ActionResult GetCabin()
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);

            List<AdminModel> cabins = new List<AdminModel>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select CabinType,Cabin_no from Tbl_Cabin where Patient_id  IS NULL AND staff_id IS NOT NULL", connection);
           // SqlCommand cmd = new SqlCommand("select Cabin_no,CabinType from Tbl_Cabin where staff_id", connection);

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable

            DataSet ds = new DataSet();
            da.Fill(ds);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AdminModel aCabin = new AdminModel();
                aCabin.Cabin_no = row["Cabin_no"].ToString();
                aCabin.CabinType = row["CabinType"].ToString();

                cabins.Add(aCabin);
            }
            connection.Close();
            var jsonString = JsonConvert.SerializeObject(new
            {
                cabins
            });

            return Json(new { data = cabins }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowCabin()
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);

            List<AdminModel> cabins = new List<AdminModel>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select c.staff_id,s.staff_name,c.Cabin_no from Tbl_cabin c inner join Tbl_Staff s on c.staff_id=s.staff_id where c.Patient_id = " + Session["Patient_id"], connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            // this will query your database and return the result to your datatable

            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AdminModel aCabin = new AdminModel();
                aCabin.staff_name = row["staff_name"].ToString();
                aCabin.Cabin_no = row["Cabin_no"].ToString();

                cabins.Add(aCabin);
            }
            connection.Close();
            var jsonString = JsonConvert.SerializeObject(new
            {
                cabins
            });

            return Json(new { data = cabins }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EditData(PatientModel Edit_info)
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["projectDatabase"].ConnectionString;
            SqlConnection connection = new SqlConnection(myConnectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("UPDATE patient SET PatientName='"+Edit_info.PatientName+"',PatientEmail='"+Edit_info.PatientEmail+"',Phone='"+Edit_info.Phone+"',Passward='"+Edit_info.Passward+"',PatientGender='"+Edit_info.PatientGender+ "'WHERE Phone="+ Edit_info.Phone, connection);
            int affectedRow = com.ExecuteNonQuery();    
            connection.Close();
            if (affectedRow==1)
            {
                return Json(affectedRow, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(affectedRow, JsonRequestBehavior.AllowGet);

            }
           
        }

    }
}