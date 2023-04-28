using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MVCADONET24223.Models;
using Newtonsoft.Json;

namespace MVCADONET24223.Controllers
{
    public class StudentController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abc"].ConnectionString);
        public ActionResult StudentForm()
        {
            return View();
        }

        public void InsertData(Student_Model obj)
        {
            if (obj.Id == 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_students", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "INSERT");
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@age", obj.Age);
                cmd.Parameters.AddWithValue("@rollno", obj.RollNo);
                cmd.Parameters.AddWithValue("@institute", obj.Institute);
                cmd.Parameters.AddWithValue("@city", obj.City);
                cmd.Parameters.AddWithValue("@cid", obj.Country);
                cmd.Parameters.AddWithValue("@sid", obj.State);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_students", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "UPDATE");
                cmd.Parameters.AddWithValue("@student_id", obj.Id);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@age", obj.Age);
                cmd.Parameters.AddWithValue("@rollno", obj.RollNo);
                cmd.Parameters.AddWithValue("@institute", obj.Institute);
                cmd.Parameters.AddWithValue("@city", obj.City);
                cmd.Parameters.AddWithValue("@cid", obj.Country);
                cmd.Parameters.AddWithValue("@sid", obj.State);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public JsonResult GetData()
        {
            string data = "";

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_students", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "GETDATA");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditRecord(Student_Model obj)
        {
            string data = "";

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_students", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "EDIT");
            cmd.Parameters.AddWithValue("@student_id", obj.Id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void DeleteRecord(Student_Model obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_students", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "DELETE");
            cmd.Parameters.AddWithValue("@student_id", obj.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public JsonResult GetCountryData()
        {
            string data = "";

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_students", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "GETCOUNTRYDATA");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStateData(Student_Model obj)
        {
            string data = "";

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_students", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "GETSTATEDATA");
            cmd.Parameters.AddWithValue("@cid", obj.Id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}